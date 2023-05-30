namespace FinalExam_Bilet7.Utilities.Extension
{
	public static class FileExtension
	{
		public static bool CheckFileType(this IFormFile file,string type)
		{
			if(file.ContentType.Contains(type))
			{
				return true;
			}
			return false;
		}
		public static bool CheckFileSize(this IFormFile file,int size)
		{
			if (file.Length <= size * 1024)
			{
				return true;
			}
			return false;
		}
		public static async Task<string> CreateFileAsync(this IFormFile file, string root, string folder)
		{
			string filename = Guid.NewGuid().ToString() + file.FileName;
			string path = Path.Combine( root, folder, filename);
			using(FileStream stream=new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			return filename;
		}
		public static void Delete(this string file,string root,string folder)
		{
			if(File.Exists(file))
			{
				File.Delete(file);
			}
		}
	}
}
