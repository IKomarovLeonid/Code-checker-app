namespace API.View
{
    public class CreateTaskRequestModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string MethodName { get; set; }

        public string NamespaceName { get; set; }
    }
}
