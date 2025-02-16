namespace VerticalFarmingApi.Services.IServices
{
    public interface ISensorImageCaptureService
    {
        /// <summary>
        /// يتم استدعاؤها لتحديث فترة التقاط الصور بالدقائق.
        /// </summary>
        /// <param name="minutes">الفترة بالدقائق.</param>
        void UpdateCaptureInterval(int minutes);
    }
}
