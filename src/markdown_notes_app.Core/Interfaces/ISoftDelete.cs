using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace markdown_notes_app.Core.Interfaces
{
    public interface ISoftDelete
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public void Undo()
        {
            IsDeleted = false;
            DeletedAt = null;
        }
    }
}
