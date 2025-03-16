// فایل اصلی پروژه برای اجرا

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using Import_Excel.Infrastructore.DbContext;
using SSO.Share.Domain.Sql.Admin.Area;

namespace MainProject
{
    /// <summary>
    /// این کلاس اصلی برنامه است که شامل متد اجرای است.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// متد اجرایی برنامه. 
        /// </summary>
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ProgramDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var dbContext = new ProgramDbContext(optionsBuilder.Options))
            {
                dbContext.Database.Migrate();
                string filePath = @"C:/Users/HP/Downloads/نشریه_تقسیمات_کشوری_وزارت_کشور_29_7_1403_مرتضوی_1.xlsx";
                ImportExcelToSql.ImportData(filePath, dbContext);
            }
        }
    }

    /// <summary>
    /// این کلاس وظیفه ی دریافت داده ها و درج آن ها در Sql را دارد .
    /// </summary>
    public class ImportExcelToSql
    {
        /// <summary>
        /// متد اصلی کلاس ImportExcelToSql.
        /// </summary>
        /// <param name="filePath">مسیر فایل اکسل .</param>
        /// <param name="dbContext">تنها DbContext پروژه .</param>
        public static void ImportData(string filePath, ProgramDbContext dbContext)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (!File.Exists(filePath))
            {
                Console.WriteLine("❌ Error: Excel file not found!");
                return;
            }

            // ساخت شیعی از کلاس Excel برای Read , Write , .... 
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    Console.WriteLine("❌ Error: The Excel file contains no worksheets!");
                    return;
                }

                var worksheet = package.Workbook.Worksheets[0];
                if (worksheet == null)
                {
                    Console.WriteLine("❌ Error: Invalid worksheet.");
                    return;
                }

                // فقط شامل هدر هستش یا نح
                int rowCount = worksheet.Dimension.Rows;
                if (rowCount < 2)
                {
                    Console.WriteLine("⚠️ Warning: No data found in the Excel file to insert.");
                    return;
                }

                var columnMappings = new Dictionary<int, string>
                {
                    { 2, "AreaTypeId (from AreaTypes.DisplayName)" },
                    { 3, "Name" },
                    { 4, "ParentId (Province)" },
                    { 5, "ParentId (County)" },
                    { 6, "ParentId (District)" },
                    { 7, "HierarchicalCode" },
                    { 8, "Code (National ID)" },
                    { 10, "Score" },
                    { 11, "Ratio" },
                    { 12, "Population" },
                };

                var areaList = new List<Area>();
                var areaDict = dbContext.Areas.ToDictionary(a => a.Name, a => a.id);

                for (int row = 2; row <= rowCount; row++)
                {
                    try
                    {
                        string areaTypeName = worksheet.Cells[row, 2].Text.Trim();
                        string name = worksheet.Cells[row, 3].Text.Trim();
                        string province = worksheet.Cells[row, 4].Text.Trim();
                        string county = worksheet.Cells[row, 5].Text.Trim();
                        string district = worksheet.Cells[row, 6].Text.Trim();
                        string hierarchicalCode = worksheet.Cells[row, 7].Text.Trim();
                        string nationalId = worksheet.Cells[row, 8].Text.Trim();
                        float score = float.TryParse(worksheet.Cells[row, 10].Text, out float s) ? s : 0;
                        float ratio = float.TryParse(worksheet.Cells[row, 11].Text, out float r) ? r : 0;
                        int population = int.TryParse(worksheet.Cells[row, 12].Text, out int p) ? p : 0;

                        var areaType = dbContext.AreaTypes.FirstOrDefault(a => a.DisplayName == areaTypeName);
                        if (areaType == null)
                        {
                            Console.WriteLine($"⚠️ Warning: Area type '{areaTypeName}' not found in the database.");
                            continue;
                        }

                        Guid? parentId = null;
                        if (!string.IsNullOrEmpty(district) && areaDict.ContainsKey(district))
                        {
                            parentId = areaDict[district];
                        }
                        else if (!string.IsNullOrEmpty(county) && areaDict.ContainsKey(county))
                        {
                            parentId = areaDict[county];
                        }
                        else if (!string.IsNullOrEmpty(province) && areaDict.ContainsKey(province))
                        {
                            parentId = areaDict[province];
                        }

                        var area = new Area
                        {
                            id = Guid.NewGuid(),
                            Name = name,
                            Code = nationalId,
                            HierarchicalCode = hierarchicalCode,
                            ParentId = parentId,
                            AreaTypeId = areaType.Id,
                            OrganizationId = "6DC91922-D68C-4174-A2F3-5B366ED8E4BC",
                            CreatedOn = DateTime.UtcNow,
                            Score = score,
                            Ratio = ratio,
                            Population = population,
                            Creator = Guid.Parse("26141497-EAA4-4D2F-8007-D6AE3363080C"),
                        };

                        areaList.Add(area);
                        areaDict[name] = area.id;

                        PrintColumnMapping(row, worksheet, columnMappings);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error processing row {row}: {ex.Message}");
                    }
                }

                if (areaList.Any())
                {
                    try
                    {
                        dbContext.Areas.AddRange(areaList);
                        dbContext.SaveChanges();
                        Console.WriteLine($"✅ {areaList.Count} rows successfully inserted into the Area table.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error saving to database: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("⚠️ Warning: No data was inserted into the database.");
                }
            }
        }
        private static void PrintColumnMapping(int row, ExcelWorksheet worksheet, Dictionary<int, string> columnMappings)
        {
            Console.WriteLine($"📌 Row {row} Mapping:");
            foreach (var mapping in columnMappings)
            {
                string cellValue = worksheet.Cells[row, mapping.Key].Text.Trim();
                Console.WriteLine($"  - Excel Column {mapping.Key}: '{cellValue}' → Database Field: {mapping.Value}");
            }
        }
    }
}
