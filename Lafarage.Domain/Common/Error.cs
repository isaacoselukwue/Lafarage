using System;
namespace Lafarage.Domain.Common;

public partial class Error
{
    public int Code { get; set; }
    public string Type { get; set; }
    public string Message { get; set; }
}