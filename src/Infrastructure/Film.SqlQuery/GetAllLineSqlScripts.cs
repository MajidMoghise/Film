namespace Film.SqlQuery
{
    public class GetAllLineSqlScripts
    {
        public static string Scripts()
        {
            string solutionDirectory = Directory.GetCurrentDirectory()
                                                .Replace("Presentation", "Infrastructure")
                                                .Replace("Film.WebAPI", "Film.SqlQuery");

            var lines1 = File.ReadAllLines(solutionDirectory + @"\StoredProcedure\Category\Category_Upldate_Bulk.sql");
            var lines2 = File.ReadAllLines(solutionDirectory + @"\StoredProcedure\Film\Film_Upldate_Bulk.sql");

            

            return String.Join("\n\r",Enumerable.Concat(lines1,lines2).ToArray());
            
            
        }
    }
}