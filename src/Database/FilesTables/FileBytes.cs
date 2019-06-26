using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace backendProject.Database.FilesTables
{
    public class FileBytes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FileId { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public long FileLength { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public byte[] Bytes { get; set; }

        [Required]
        public Boolean IsPublic { get; set; } = false;

        public virtual ICollection<Read> ReadPermissions { get; set; }
        public virtual ICollection<Write> WritePermissions { get; set; }
    }
}