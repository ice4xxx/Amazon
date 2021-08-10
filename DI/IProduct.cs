using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public interface IProduct
    {
        //Product's id.
        int Id { get; set; }


        /// <summary>
        /// Product's name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Product's cost.
        /// </summary>
        int Cost { get; set; }

        /// <summary>
        /// Product's image in base64 string.
        /// </summary>
        string Base64Image { get; set; }
    }
}
