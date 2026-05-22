namespace ConsoleAppDotnetCore.GenerateCsvFileDemo
{
    internal class GenerateCsvFileDemoRun
    {
        // Adjust these to scale the dataset size
        const int UserCount = 1000;
        const int CompanyCount = 50;
        const int SkillCount = 20;
        const int JobCount = 200;

        public static void Run()
        {

            Random rng = new Random();
            string outputDir = "./Output";
            Directory.CreateDirectory(outputDir);

            Console.WriteLine("Generating CSV files...");

            // 1. Generate Users
            using (var writer = new StreamWriter($"{outputDir}/users.csv"))
            {
                writer.WriteLine("id,name,email,headline,createdAt");
                for (int i = 1; i <= UserCount; i++)
                    writer.WriteLine($"{i},User{i},user{i}@example.com,Software Dev {i},{DateTime.Now.AddDays(-rng.Next(1, 365)):yyyy-MM-dd}");
            }

            // 2. Generate Companies
            using (var writer = new StreamWriter($"{outputDir}/companies.csv"))
            {
                writer.WriteLine("id,name,industry");
                for (int i = 1; i <= CompanyCount; i++)
                    writer.WriteLine($"{i},Company{i},Tech");
            }

            // 3. Generate Skills
            using (var writer = new StreamWriter($"{outputDir}/skills.csv"))
            {
                writer.WriteLine("id,name");
                string[] skills = { "C#", "Neo4j", "SQL", "Docker", "AWS", "React", "Python", "Java" };
                for (int i = 1; i <= SkillCount; i++)
                    writer.WriteLine($"{i},Skill_{i}_{skills[rng.Next(skills.Length)]}");
            }

            // 4. Generate Jobs
            using (var writer = new StreamWriter($"{outputDir}/jobs.csv"))
            {
                writer.WriteLine("id,companyId,title,description,createdAt");
                for (int i = 1; i <= JobCount; i++)
                    writer.WriteLine($"{i},{rng.Next(1, CompanyCount + 1)},Job {i},Description for job {i},{DateTime.Now.AddDays(-rng.Next(1, 30)):yyyy-MM-dd}");
            }

            // 5. Generate Relationships
            using (var writer = new StreamWriter($"{outputDir}/relationships.csv"))
            {
                writer.WriteLine("sourceId,targetId,type,meta");

                // CONNECTED (User-User)
                for (int i = 0; i < 2000; i++)
                    writer.WriteLine($"{rng.Next(1, UserCount)},{rng.Next(1, UserCount)},CONNECTED,Since:2025");

                // WORKS_AT (User-Company)
                for (int i = 1; i <= UserCount; i++)
                    writer.WriteLine($"{i},{rng.Next(1, CompanyCount + 1)},WORKS_AT,Role:Employee");

                // HAS_SKILL (User-Skill)
                for (int i = 0; i < 3000; i++)
                    writer.WriteLine($"{rng.Next(1, UserCount)},{rng.Next(1, SkillCount + 1)},HAS_SKILL,Level:Expert");

                // POSTED_JOB (Company-Job)
                for (int i = 1; i <= JobCount; i++)
                    writer.WriteLine($"{rng.Next(1, CompanyCount + 1)},{i},POSTED_JOB,Date:2026-01-01");

                // APPLIED_TO (User-Job)
                for (int i = 0; i < 1500; i++)
                    writer.WriteLine($"{rng.Next(1, UserCount)},{rng.Next(1, JobCount + 1)},APPLIED_TO,Status:Pending");

                // REQUIRES (Job-Skill)
                for (int i = 0; i < 500; i++)
                    writer.WriteLine($"{rng.Next(1, JobCount + 1)},{rng.Next(1, SkillCount + 1)},REQUIRES,Mandatory:True");
            }

            Console.WriteLine("Done! CSV files are in the /Output folder.");
        }
    }
}
