﻿using System;
namespace TodoApiDTO.Application.Interfaces.Services.Models
{
    public class TodoItemEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}
