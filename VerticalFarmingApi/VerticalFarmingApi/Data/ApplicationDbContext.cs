using Microsoft.EntityFrameworkCore;
using VerticalFarmingApi.Models;

namespace VerticalFarmingApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AIAnalysisResult> AIAnalysisResults { get; set; }
        public DbSet<DiseaseAlert> DiseaseAlerts { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorData> SensorDatas { get; set; }
        public DbSet<CropHealthReport> CropHealthReports { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<SensorImage> SensorImages { get; set; }
        //public DbSet<ApplicationUser> applicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // يمكن إضافة تكوينات إضافية هنا (مثل العلاقات أو الفهارس)
            base.OnModelCreating(modelBuilder);
        }
    }
       
}
