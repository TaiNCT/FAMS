namespace TrainingProgramManagementAPI.Payloads
{
    public static class APIRoutes
    {
        public const string Base = "api";

        public static class TrainingProgram
        {
            // Get All
            public const string GetAll = Base + "/trainingprograms/";

            // Get ALl Author of Training Program 
            public const string GetALlAuthors = Base + "/trainingprograms/authors";

            // Get by Code
            public const string GetByCode = Base + "/trainingprograms/{code}";

            // Get Training Program and Syllabus 
            public const string GetProgramSyllabusByCode = Base + "/trainingprograms/syllabus/{code}";

            // Filter
            public const string Filter = Base + "/trainingprograms/filter";

            // Delete
            public const string Delete = Base + "/trainingprograms/delete/{code}";

            //Duplicate
            public const string Duplicate = Base + "/trainingprograms/duplicate";

            // Update 
            public const string Update = Base + "/trainingprograms/update";

            // Search
            public const string Search = Base + "/trainingprograms/search";

            //Sorting
            public const string Sorting = Base + "/trainingprograms/sorting";

            // Create 
            public const string Create = Base + "/trainingprograms";


            // Download material of training program
            public const string DownloadMaterial = Base + "/trainingprograms/{code}/materials";

            // Download material of training program
            public const string DownloadSingleMaterial = Base + "/trainingprograms/materials/{id}";

            // Upload material 
            public const string UploadMaterial = Base + "/trainingprograms/materials";

            // Delete material
            public const string DeleteMaterial = Base + "/trainingprograms/materials";

            // Update material 
            public const string UpdateMaterial = Base + "/trainingprograms/materials";

            // Get all material
            public const string GetMaterial = Base + "/trainingprograms/materials";

            // Export to excel        
            public const string ExportExcel = Base + "/trainingprograms/export";

            // Import File 
            public const string ImportFile = Base + "/trainingprograms/uploadFile";
        }
    }
}