using markdown_notes_app.Core.Entities;
using markdown_notes_app.Core.Interfaces.Common;
using markdown_notes_app.Core.Results;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace markdown_notes_app.Infrastructure.ExternalServices
{
    /**
     * When making requests, you can authenticate by including your API key as a key parameter 
     *  or by providing it as a bearer token in the Authorization header. 
     * If both are supplied, the key parameter takes precedence.
     * 
     * Request Parameters
     * ==================
     * 
     * *key - str
     * *text - str
     * session_id - optional str [Use title to enable Sapling to persist]
     *      Unique name or UUID of document or portion of text that is being checked.
     *      If not provided, defaults to current date in YYYYMMDD format.
     * user_id - str
     *      Use this along with the accept and reject endpoints to allow different users to maintain individual preferences.
     * lang - str
     *      [auto: Autodetect language, en, ar, bg, ca, cs, da, de, el, es, et, fa, 
     *      fi, fr, he, hi, hr, hu, id, is, it, jp, ja, ko, lt, lv, nl, no, pl, 
     *      ro, ru, sk, sq, sr, sv, th, tl, tr, uk, vi, zh]
     * variety - str
     *      This option is currently available for: English, Chinese (中文). 
     *      Specifies regional English/Chinese spelling options. 
     *      The setting defaults to the configuration in the Sapling dashboard.
     *      lang=en [us-variety, gb-variety, au-variety, ca-variety, null-variety]
     *      lang=zh [s-variety, t-variety]
     * auto_apply - bool
     *      Default is false. 
     *      If true, result with have extra field applied_text containing text with edits applied.
     * neural_spellcheck - bool
     * operations - str[]
     *      [capitalize, punctuate, fixspace, split]
     * medical - bool
     * 
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
    public class SaplingAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey = "";
        private readonly ILoggerManager<SaplingAPIClient> _loggerManager;
        private readonly string saplingApiBaseURL = "https://api.sapling.ai/api/v1/spellcheck";
        public SaplingAPIClient(IHttpClientFactory httpClientFactory, ILoggerManager<SaplingAPIClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _loggerManager = logger;
        }

        public async Task<Result<SaplingResponse>>? PostSpellCheckAsync(string text, object jsonBody)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("SaplingAPIClient");
                var response = await client.PostAsJsonAsync($@"{saplingApiBaseURL}?key={_apiKey}", jsonBody);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return ResultFactory.Error<SaplingResponse>(errorContent, (int)response.StatusCode, "API Request Failed");
                }

                var jsonContent = await response.Content.ReadAsStringAsync();
                var saplingResp = JsonSerializer.Deserialize<SaplingResponse>(jsonContent);

                return ResultFactory.Success<SaplingResponse>(saplingResp);
            }
            catch (HttpRequestException httpEx)
            {
                //log - More Robust Errors Saying Where My Error is
                return ResultFactory.Exception<SaplingResponse>(httpEx.Message, 500, "Network Error");
            }
            catch (Exception ex)
            {
                //log
                return ResultFactory.Exception<SaplingResponse>(ex.Message, 500, "Server Failure");
            }
        }

    }
}
