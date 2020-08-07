using System.Globalization;
using Nano.Template.Web.Translation.Resources.Exceptions;

namespace Nano.Template.Web.Translation.Enums.Extensions
{
    /// <summary>
    /// Translation Code Extensions.
    /// </summary>
    public static class TranslationCodeExtensions
    {
        /// <summary>
        /// Returns the translated string for the specified <see cref="CultureInfo"/>, using the passed <see cref="CultureInfo"/>.
        /// If no <see cref="CultureInfo.CurrentCulture"/> is passed, the <see cref="CultureInfo"/> is used.
        /// </summary>
        /// <param name="code">The <see cref="TranslationCode"/>.</param>
        /// <param name="cultureInfo">The <see cref="TranslationCode"/> (optional).</param>
        /// <returns>The translated text.</returns>
        public static string GetTranslation(this TranslationCode code, CultureInfo cultureInfo = null)
        {
            cultureInfo ??= CultureInfo.CurrentCulture;

            return Exceptions.ResourceManager
                .GetString(code.ToString(), cultureInfo);
        }
    }
}