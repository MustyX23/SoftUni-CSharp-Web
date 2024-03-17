namespace SeminarHub.Data.Common
{
    public static class EntityValidationConstants
    {
        public static class Seminar
        {
            public const int TopicMinLenght = 3;
            public const int TopicMaxLenght = 100;

            public const int LecturerMinLenght = 5;
            public const int LecturerMaxLenght = 60;

            public const int DetailsMinLenght = 10;
            public const int DetailsMaxLenght = 500;

            public const int DurationMin = 30;
            public const int DurationMax = 180;
        }
        public static class Category
        {
            public const int NameMinLenght = 3;
            public const int NameMaxLenght = 50;
            
        }
    }
}
