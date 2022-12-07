using AForge.Imaging;
using KiwiClickerBot.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwiClickerBot.Services
{
    public class AforgeService
    {
        //found matchings
        private List<TemplateMatch> _matchings;
        private readonly string pathOriginal;
        private readonly string pathSample;

        /// <summary>
        /// matchings counter
        /// </summary>
        public int CountMatchings
        {
            get => _matchings != null ? _matchings.Count : 0;
        }


        //ctor
        public AforgeService(string pathOriginal, string pathSample)
        {
            this.pathOriginal = pathOriginal;
            this.pathSample = pathSample;
        }

        /// <summary>
        /// Checks original image contains sample image
        /// </summary>
        /// <param name="pathOriginalImage">path to the original image</param>
        /// <param name="pathSampleImage">path to the sample image</param>
        /// <returns>true if does</returns>
        public async Task<bool> IsContains(string pathOriginalImage, string pathSampleImage)
        {
            if (string.IsNullOrEmpty(pathOriginalImage)) throw new ArgumentNullException(nameof(pathOriginalImage));
            if (string.IsNullOrEmpty(pathSampleImage)) throw new ArgumentNullException(nameof(pathSampleImage));

            var sample = new Bitmap(pathSampleImage);
            var orig = new Bitmap(pathOriginalImage);

            //Aforge library
            ExhaustiveTemplateMatching tm = new(0.921f);
            var tmatchings = await Task.Run(() => tm.ProcessImage(orig, sample));
            _matchings = tmatchings.ToList();

            return _matchings.Any();
        }


        /// <summary>
        /// Get collection of found places
        /// </summary>
        public List<FoundPlace> GetPlaces()
        {
            List<FoundPlace> result = new List<FoundPlace>();
            if (CountMatchings == 0) return result;

            int id = 0;
            foreach (var match in _matchings)
            {
                FoundPlace place = new()
                {
                    Id = ++id,
                    Similarity = match.Similarity,
                    Top = match.Rectangle.Top,
                    Left = match.Rectangle.Left,
                    Height = match.Rectangle.Height,
                    Width = match.Rectangle.Width
                };

                result.Add(place);
            }

            return result;
        }
    }
}
