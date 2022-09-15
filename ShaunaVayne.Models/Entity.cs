using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShaunaVayne.Common.Filters;

namespace ShaunaVayne.Models
{
    public class Entity
    {
        [Key]
        [SwaggerIgnore]
        [BindNever]
        public Guid Id { get; set; } = Guid.NewGuid();

        [SwaggerIgnore]
        [BindNever]
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    }
}