using System;
using System.IO;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

class ImageProcessor
{
    static void Main()
    {
        Console.WriteLine("Введите путь к папке с изображениями:");
        string folderPath = Console.ReadLine();

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine("Указанная папка не существует!");
            return;
        }

        Console.WriteLine("Введите максимальный размер большей стороны (в пикселях):");
        if (!int.TryParse(Console.ReadLine(), out int maxSide) || maxSide <= 0)
        {
            Console.WriteLine("Некорректный размер!");
            return;
        }

        bool flipHorizontal = false;
        while (true)
        {
            Console.WriteLine("Отразить по горизонтали? (1 - да, 0 - нет):");
            string flipInput = Console.ReadLine();
            if (flipInput == "1")
            {
                flipHorizontal = true;
                break;
            }
            else if (flipInput == "0")
            {
                flipHorizontal = false;
                break;
            }
            else
            {
                Console.WriteLine("Некорректный ввод! Введите 1 или 0.");
            }
        }

        string outputFolder = Path.Combine(folderPath, "resized");
        Directory.CreateDirectory(outputFolder);

        var validExtensions = new[] { ".png", ".bmp", ".gif", ".jpg", ".jpeg", ".webp", ".avif" };
        var imageFiles = Directory.GetFiles(folderPath)
            .Where(f => validExtensions.Contains(Path.GetExtension(f).ToLower()))
            .ToList();

        int processed = 0;
        int skipped = 0;
        int total = imageFiles.Count;
        int digits = total.ToString().Length;

        foreach (var (file, index) in imageFiles.Select((f, i) => (f, i)))
        {
            string newName = (index + 1).ToString($"D{digits}") + ".jpg";
            string outputPath = Path.Combine(outputFolder, newName);

            try
            {
                using (var image = Image.Load(file))
                {
                    int originalWidth = image.Width;
                    int originalHeight = image.Height;
                    int maxDimension = Math.Max(originalWidth, originalHeight);

                    // Вычисляем целевой размер (кратный 64)
                    int targetSize = (maxSide / 64) * 64;
                    if (targetSize > maxDimension) targetSize = (maxDimension / 64) * 64;

                    // Рассчитываем новые размеры
                    double scale = (double)targetSize / maxDimension;
                    int newWidth = (int)Math.Round(originalWidth * scale);
                    int newHeight = (int)Math.Round(originalHeight * scale);

                    // Корректируем размеры до кратных 64
                    newWidth = (newWidth / 64) * 64;
                    newHeight = (newHeight / 64) * 64;

                    // Проверка минимального размера
                    if (newWidth < 64 || newHeight < 64)
                    {
                        Console.WriteLine($"Слишком маленькое изображение: {Path.GetFileName(file)}");
                        skipped++;
                        continue;
                    }

                    // Ресайз и кроп
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(newWidth, newHeight),
                        Mode = ResizeMode.Crop,
                        Sampler = KnownResamplers.Lanczos3
                    }));

                    // Отражение по горизонтали
                    if (flipHorizontal)
                    {
                        image.Mutate(x => x.Flip(FlipMode.Horizontal));
                    }

                    // Сохранение в JPEG
                    image.Save(outputPath, new JpegEncoder { Quality = 90 });
                    processed++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обработки {Path.GetFileName(file)}: {ex.Message}");
                skipped++;
            }
        }

        // Отчет о результатах
        Console.WriteLine("\nРезультаты обработки:");
        Console.WriteLine($"Всего изображений: {total}");
        Console.WriteLine($"Успешно обработано: {processed}");
        Console.WriteLine($"Пропущено: {skipped}");
        Console.WriteLine($"Режим отражения: {(flipHorizontal ? "ВКЛЮЧЕН" : "ВЫКЛЮЧЕН")}");
    }
}