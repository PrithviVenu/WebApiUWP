﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public class ComicProcessor
    {
        public int MaxComicNumber { get; set; }
        public static async Task<ComicModel> LoadComic(int comicNumber = 0)
        {
            string url;
            if (comicNumber > 0)
            {
                url = $"http://xkcd.com/{ comicNumber }/info.0.json";
            }
            else
            {
                url = "http://xkcd.com/info.0.json";
            }
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ComicModel comic = await response.Content.ReadAsAsync<ComicModel>();

                    return comic;

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
