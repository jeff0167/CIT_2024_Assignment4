﻿namespace WebApi.Models
{
    public class CategoryModel
    {
        public string? Url { get; set; }
        public string Name { get; set; } = string.Empty; //ensures that Name will be initialized to an empty string, if not provided.
        public string? Description { get; set; }
    }
}
