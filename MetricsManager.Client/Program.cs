namespace MetricsManager.Client
{
    internal static class Program
    {
        private const string LocalAdress = "http://localhost:5159";
    
        static readonly CpuMetricsClient CpuMetricsClient = new CpuMetricsClient(LocalAdress, new HttpClient());
        static readonly DotNetMetricsClient DotNetMetricsClient = new DotNetMetricsClient(LocalAdress, new HttpClient());
        static readonly HddMetricsClient HddMetricsClient = new HddMetricsClient(LocalAdress, new HttpClient());
        static readonly NetworkMetricsClient NetworkMetricsClient = new NetworkMetricsClient(LocalAdress, new HttpClient());
        static readonly RamMetricsClient RamMetricsClient = new RamMetricsClient(LocalAdress, new HttpClient());

        private static async Task Main()
        {
            while (true)
            {
                Console.Clear();
                
                Console.WriteLine("Задачи:");
                Console.WriteLine("1) Получить метрики за последнюю минуту (CPU)");
                Console.WriteLine("2) Получить метрики за последнюю минуту (DotNet)");
                Console.WriteLine("3) Получить метрики за последнюю минуту (Hdd)");
                Console.WriteLine("4) Получить метрики за последнюю минуту (Network)");
                Console.WriteLine("5) Получить метрики за последнюю минуту (Ram)");
                Console.WriteLine("0) Завершение работы приложения");
                Console.WriteLine();
                Console.Write("Введите номер: ");

                if (!int.TryParse(Console.ReadLine(), out var taskNumber))
                    return;

                switch (taskNumber)
                {
                    case 0:
                        Console.WriteLine("Завершение работы приложения.");
                        Console.ReadKey(true);
                        Environment.Exit(1);
                        break;
                    case 1:
                        try
                        {
                            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);

                            CpuMetricsResponse response = await CpuMetricsClient.GetAllByIdAsync(
                                1,
                                fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                toTime.ToString("dd\\.hh\\:mm\\:ss"));

                            foreach (CpuMetric metric in response.Metrics)
                            {
                                Console.WriteLine(
                                    $"{TimeSpan.FromSeconds(metric.Time):dd\\.hh\\:mm\\:ss} >>> {metric.Value}");
                            }

                            Console.WriteLine("Нажмите любую клавишу для продолжения работы ...");
                            Console.ReadKey(true);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Произошла ошибка при попыте получить CPU метрики.\n{e.Message}");
                        }

                        break;
                    case 2:
                        try
                        {
                            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);

                            DotNetMetricsResponse response = await DotNetMetricsClient.GetAllByIdAsync(
                                1,
                                fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                toTime.ToString("dd\\.hh\\:mm\\:ss"));

                            foreach (DotNetMetric metric in response.Metrics)
                            {
                                Console.WriteLine(
                                    $"{TimeSpan.FromSeconds(metric.Time):dd\\.hh\\:mm\\:ss} >>> {metric.Value}");
                            }

                            Console.WriteLine("Нажмите любую клавишу для продолжения работы ...");
                            Console.ReadKey(true);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Произошла ошибка при попыте получить CPU метрики.\n{e.Message}");
                        }

                        break;
                    case 3:
                        try
                        {
                            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);

                            HddMetricsResponse response = await HddMetricsClient.GetAllByIdAsync(
                                1,
                                fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                toTime.ToString("dd\\.hh\\:mm\\:ss"));

                            foreach (HddMetric metric in response.Metrics)
                            {
                                Console.WriteLine(
                                    $"{TimeSpan.FromSeconds(metric.Time):dd\\.hh\\:mm\\:ss} >>> {metric.Value}");
                            }

                            Console.WriteLine("Нажмите любую клавишу для продолжения работы ...");
                            Console.ReadKey(true);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Произошла ошибка при попыте получить CPU метрики.\n{e.Message}");
                        }

                        break;
                    case 4:
                        try
                        {
                            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);

                            NetworkMetricsResponse response = await NetworkMetricsClient.GetAllByIdAsync(
                                1,
                                fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                toTime.ToString("dd\\.hh\\:mm\\:ss"));

                            foreach (NetworkMetric metric in response.Metrics)
                            {
                                Console.WriteLine(
                                    $"{TimeSpan.FromSeconds(metric.Time):dd\\.hh\\:mm\\:ss} >>> {metric.Value}");
                            }

                            Console.WriteLine("Нажмите любую клавишу для продолжения работы ...");
                            Console.ReadKey(true);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Произошла ошибка при попыте получить CPU метрики.\n{e.Message}");
                        }

                        break;
                    case 5:
                        try
                        {
                            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);

                            RamMetricsResponse response = await RamMetricsClient.GetAllByIdAsync(
                                1,
                                fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                toTime.ToString("dd\\.hh\\:mm\\:ss"));

                            foreach (RamMetric metric in response.Metrics)
                            {
                                Console.WriteLine(
                                    $"{TimeSpan.FromSeconds(metric.Time):dd\\.hh\\:mm\\:ss} >>> {metric.Value}");
                            }

                            Console.WriteLine("Нажмите любую клавишу для продолжения работы ...");
                            Console.ReadKey(true);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Произошла ошибка при попыте получить CPU метрики.\n{e.Message}");
                        }

                        break;
                    default:
                        Console.WriteLine("Введите правильный номер подзадачи.");
                        break;
                }
            }
        }
    }
}