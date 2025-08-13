# TImage Preprocessor for LoRA Training

Консольное приложение для подготовки изображений к обучению LoRA (Low-Rank Adaptation) моделей. Автоматически обрабатывает изображения в папке, приводя их к размерам, кратным 64 пикселям - стандартному требованию для современных моделей стабильной диффузии.

A console application for preparing images for training LoRA (Low-Rank Adaptation) models. Automatically processes images in a folder, resizing them to dimensions divisible by 64 pixels - a standard requirement for modern stable diffusion models.

## Ключевые функции / Key Features

- **Автоматическое масштабирование**: Пропорционально изменяет размер изображений до заданного значения по большей стороне  
  **Automatic Scaling**: Proportionally resizes images to the specified value on the larger side
  
- **Кратность 64**: Гарантирует, что обе стороны изображения будут кратны 64 пикселям  
  **64 Divisibility**: Ensures both sides of the image are divisible by 64 pixels
  
- **Интеллектуальное кадрирование**: Центрированное обрезание изображений при необходимости  
  **Smart Cropping**: Centered cropping of images when necessary
  
- **Опциональное отражение**: Возможность зеркального отражения всех изображений  
  **Optional Flipping**: Ability to flip all images horizontally
  
- **Пакетная обработка**: Автоматическая обработка всех изображений в указанной папке  
  **Batch Processing**: Automatic processing of all images in the specified folder
  
- **Стандартизированное именование**: Автоматическая нумерация файлов с ведущими нулями  
  **Standardized Naming**: Automatic file numbering with leading zeros
  
- **Контроль качества**: Сохранение в JPEG с качеством 90%  
  **Quality Control**: Saving in JPEG with 90% quality

## Требования / Requirements

- **ОС**: Windows 10/11  
  **OS**: Windows 10/11
  
- **Платформа**: .NET 6.0 Runtime или выше ([скачать](https://dotnet.microsoft.com/en-us/download/dotnet/6.0))  
  **Platform**: .NET 6.0 Runtime or higher ([download](https://dotnet.microsoft.com/en-us/download/dotnet/6.0))

## Использование / Usage

1. **Запустите приложение**  
   **Run the application**
   
2. **Введите путь к папке с изображениями**  
   **Enter the path to the image folder**
   
3. **Укажите максимальный размер большей стороны (рекомендуется 512, 768 или 1024)**  
   **Specify the maximum size of the larger side (recommended 512, 768 or 1024)**
   
4. **Выберите нужно ли отражать изображения (1 - да, 0 - нет)**  
   **Choose whether to flip images (1 - yes, 0 - no)**
   
5. **Обработанные изображения появятся в подпапке `resized`**  
   **Processed images will appear in the `resized` subfolder**

P.S. The author's slippers were sacrificed to neural networks during this translation...
