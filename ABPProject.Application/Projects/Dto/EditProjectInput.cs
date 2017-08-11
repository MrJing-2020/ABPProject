using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace ABPProject.Projects.Dto
{
    [AutoMap(typeof(Project))]
    public class EditProjectInput
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
