using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Response Object
 * ===============
 * Response is an array of edit objects
 *
 * {
 *  "edits": [
 *      {
 *          "end": 22,
 *          "error_type": "R:PUNCT",
 *          "general_error_type": "Punctuation",
 *          "id": "4bb963a4-cc19-523e-9bb2-9e6b5a270bfc",
 *          "replacement": "doing?",
 *          "sentence": "Hi, how are you doing.",
 *          "sentence_start": 0,
 *          "start": 16
 *      }
 *    ]
 *  }
 * 
 *   Edit Object:-
 *   
 *   {
 *      "id": <str, UUID>,           // Opaque edit id, used to give feedback
 *      "sentence": <str>,           // Unedited sentence
 *      "sentence_start": <int>,
 *      "start": <int>,
 *      "end": <int>,
 *      "replacement": <str>,
 *      "error_type": <str>,
 *      "general_error_type": <str>,
 *      "applied_text": <str>         // Optional put string.Empty by default
 *   }
 */
namespace markdown_notes_app.Core.Entities
{
    public class Edit()
    {
        public string id { get; set; }
        public string? Sentence { get; set; }
        public int? sentence_start { get; set; }
        public int? start {  get; set; }
        public int? end { get; set; }
        public string? replacement { get; set; }
        public string? error_type { get; set; }
        public string? general_error_type { get; set; }
        public string? applied_text { get; set; } = null;
    }


    public class SaplingResponse
    {
        public List<Edit> edits { get; set; } = new List<Edit>();
    }
}
