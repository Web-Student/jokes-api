using System.ComponentModel.DataAnnotations.Schema;

namespace Data;
[Table("joke")]
public class Joke {
    public int id {get;set;}
    public string? author {get;set;}
    public string? question {get;set;}
    public string? answer {get;set;}
}