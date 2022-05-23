using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Training.Domain;
public class TodoItem
{
    public Guid Id { get; set; }

    public string Text { get; set; } = default!;

    public bool IsCompleted { get; set; }
}
