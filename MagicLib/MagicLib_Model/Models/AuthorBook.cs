using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;



namespace MagicLib_Model.Models
{
    [Table("AuthorBook")]
    [Index(nameof(BooksBookId), Name = "IX_AuthorBook_BooksBook_Id")]
    public partial class AuthorBook
    {
        [Key]
        [Column("AuthorsAuthor_Id")]
        public int AuthorsAuthorId { get; set; }
        [Key]
        [Column("BooksBook_Id")]
        public int BooksBookId { get; set; }
    }
}

// Was able to import this table on PMC with: 

// Scaffold-Dbcontext "Server=DESKTOP-3M9V0GN\SQLEXPRESS;Database=MagicLib;Trusted_Connection=True;MultipleActiveResultSets=True" Microsoft.EntityFrameworkCore.SqlServer -Outputdir Models -Context TESTCONTEXT -Tables AuthorBook -dataannotations


// More on:

// https://www.youtube.com/watch?v=iGJN34XGvgk
// https://www.youtube.com/watch?v=-sftSA9_X-k
// https://stackoverflow.com/questions/17650482/instance-failure-error-while-connection-string-is-correct
// Last URL I understood I was using 1 \ too much.