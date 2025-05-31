using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    public static class RandomProductGenerator
    {
        private static readonly Random random = new Random();
        public static Product GenerateRandomProduct()
        {
            int productType = random.Next(1, 9);

            return productType switch
            {
                1 => GenerateRandomNovel(),
                2 => GenerateRandomBiography(),
                3 => GenerateRandomAstrophysicsMagazine(),
                4 => GenerateRandomFashionMagazine(),
                5 => GenerateRandomTextbook(),
                6 => GenerateRandomShortStoryCollection(),
                7 => GenerateRandomTravelMagazine(),
                8 => GenerateRandomMedicalMagazine(),
                _ => GenerateRandomNovel()
            };
        }
        private static readonly string[] Names =
        {
        "Таємниця зірок", "Квантова фізика", "Модні тенденції",
        "Історичні постаті", "Науково фантастичні пригоди", "Космічні відкриття",
        "Тенденції подіумів", "Великі вчені", "Фентезійні світи", "Технічні інновації"
    };

        private static readonly string[] Authors =
        {
        "Іван Петренко", "Олена Шевченко", "Михайло Коваленко", "Наталія Бойко",
        "Данило Мельник", "Анна Коваль", "Роман Лисенко", "Юлія Ткаченко"
    };

        private static readonly string[] Publishers =
        {
        "Наукова преса", "Фентезі світ", "Модні медіа", "Історичні книги",
        "Космічні видання", "Технічні видання", "Літературний світ", "Академвидав"
    };

        private static readonly string[] Genres =
        {
        "Фентезі", "Наукова фантастика", "Детектив", "Романтика", "Трилер"
    };

        private static readonly string[] Audiences =
        {
        "Діти", "Підлітки", "Дорослі", "Літні люди", "Всі вікові групи"
    };

        private static readonly string[] Topics =
        {
        "Наука", "Історія", "Технології", "Мистецтво", "Політика", "Філософія"
    };

        private static readonly string[] ScientificValues =
        {
        "Висока", "Середня", "Низька"
    };

        private static readonly string[] Periodicities =
        {
        "Щомісячний", "Квартальний", "Двомісячний", "Щотижневий", "Щорічний"
    };

        private static readonly string[] Trends =
        {
        "Весняна колекція", "Літні стилі", "Осіння мода", "Зимові тенденції",
        "Кольорові палітри", "Тенденції аксесуарів", "Мода взуття"
    };

        private static readonly string[] Discoveries =
        {
        "Нова екзопланета", "Дослідження чорних дір", "Відкриття темної матерії",
        "Квантові обчислення", "Дослідження космосу", "Формування галактик"
    };

        private static readonly string[] Subjects =
        {
        "Математика", "Фізика", "Хімія", "Біологія", "Історія",
        "Географія", "Англійська мова", "Українська література"
    };

        private static readonly string[] GradeLevels =
        {
        "1 клас", "2 клас", "3 клас", "4 клас", "5 клас",
        "6 клас", "7 клас", "8 клас", "9 клас", "10 клас",
        "11 клас", "1 курс", "2 курс", "3 курс", "4 курс"
    };

        private static readonly string[] CoverStyles =
        {
        "М'яка", "Тверда", "Гнучка", "Шкіряна"
    };

        private static readonly string[] Regions =
        {
        "Європа", "Азія", "Африка", "Північна Америка",
        "Південна Америка", "Австралія", "Антарктида"
    };

        private static readonly string[] PhotographyQualities =
        {
        "Висока", "Середня", "Низька"
    };

        private static readonly string[] MedicalSpecialties =
        {
        "Кардіологія", "Педіатрія", "Хірургія", "Терапія",
        "Неврологія", "Дерматологія", "Офтальмологія"
    };

        private static readonly string[] TargetProfessionals =
        {
        "Лікарі", "Медсестри", "Фармацевти", "Студенти медичних ВНЗ"
    };

        public static List<Product> GenerateRandomProducts()
        {
            Console.Write("Введіть кількість випадкових продуктів для генерації: ");
            int count;
            while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
            {
                Console.Write("Некоректне введення. Будь ласка, введіть додатне число: ");
            }

            var products = new List<Product>();

            for (int i = 0; i < count; i++)
            {
                int productType = random.Next(1, 9);

                switch (productType)
                {
                    case 1:
                        products.Add(GenerateRandomNovel());
                        break;
                    case 2:
                        products.Add(GenerateRandomBiography());
                        break;
                    case 3:
                        products.Add(GenerateRandomAstrophysicsMagazine());
                        break;
                    case 4:
                        products.Add(GenerateRandomFashionMagazine());
                        break;
                    case 5:
                        products.Add(GenerateRandomTextbook());
                        break;
                    case 6:
                        products.Add(GenerateRandomShortStoryCollection());
                        break;
                    case 7:
                        products.Add(GenerateRandomTravelMagazine());
                        break;
                    case 8:
                        products.Add(GenerateRandomMedicalMagazine());
                        break;
                }
            }

            return products;
        }

        private static Textbook GenerateRandomTextbook()
        {
            return new Textbook(
                name: $"Підручник {Subjects[random.Next(Subjects.Length)]}",
                price: random.Next(200, 600),
                author: Authors[random.Next(Authors.Length)],
                publisher: Publishers[random.Next(Publishers.Length)],
                topic: Subjects[random.Next(Subjects.Length)],
                scientificValue: ScientificValues[random.Next(ScientificValues.Length)],
                subject: Subjects[random.Next(Subjects.Length)]
            );
        }

        private static ShortStoryCollection GenerateRandomShortStoryCollection()
        {
            return new ShortStoryCollection(
                name: $"Збірка оповідань {Authors[random.Next(Authors.Length)]}",
                price: random.Next(150, 400),
                author: Authors[random.Next(Authors.Length)],
                publisher: Publishers[random.Next(Publishers.Length)],
                genre: Genres[random.Next(Genres.Length)],
                targetAudience: Audiences[random.Next(Audiences.Length)],
                storyCount: random.Next(5, 30)
            );
        }

        private static TravelMagazine GenerateRandomTravelMagazine()
        {
            return new TravelMagazine(
                name: $"Подорожі {Regions[random.Next(Regions.Length)]}",
                price: random.Next(100, 300),
                periodicity: Periodicities[random.Next(Periodicities.Length)],
                publisher: Publishers[random.Next(Publishers.Length)],
                topic: "Подорожі",
                targetAudience: random.Next(0, 2) == 0 ? "Дорослі" : "Сім'ї",
                primaryRegion: Regions[random.Next(Regions.Length)]
            );
        }

        private static MedicalMagazine GenerateRandomMedicalMagazine()
        {
            return new MedicalMagazine(
                name: $"Медицина {MedicalSpecialties[random.Next(MedicalSpecialties.Length)]}",
                price: random.Next(300, 500),
                periodicity: Periodicities[random.Next(Periodicities.Length)],
                publisher: Publishers[random.Next(Publishers.Length)],
                topic: MedicalSpecialties[random.Next(MedicalSpecialties.Length)],
                scientificValue: "Висока",
                medicalSpecialty: MedicalSpecialties[random.Next(MedicalSpecialties.Length)]
            );
        }

        private static Novel GenerateRandomNovel()
        {
            return new Novel(
                name: Names[random.Next(Names.Length)],
                price: random.Next(100, 500),
                author: Authors[random.Next(Authors.Length)],
                publisher: Publishers[random.Next(Publishers.Length)],
                genre: Genres[random.Next(Genres.Length)],
                targetAudience: Audiences[random.Next(Audiences.Length)],
                pageCount: random.Next(200, 800)
            );
        }

        private static Biography GenerateRandomBiography()
        {
            return new Biography(
                name: $"Біографія {Authors[random.Next(Authors.Length)]}",
                price: random.Next(150, 600),
                author: Authors[random.Next(Authors.Length)],
                publisher: Publishers[random.Next(Publishers.Length)],
                topic: Topics[random.Next(Topics.Length)],
                scientificValue: ScientificValues[random.Next(ScientificValues.Length)],
                aboutPerson: Authors[random.Next(Authors.Length)]
            );
        }

        private static AstrophysicsMagazine GenerateRandomAstrophysicsMagazine()
        {
            return new AstrophysicsMagazine(
                name: $"Астро {Names[random.Next(Names.Length)]}",
                price: random.Next(200, 400),
                periodicity: Periodicities[random.Next(Periodicities.Length)],
                publisher: Publishers[random.Next(Publishers.Length)],
                topic: "Астрофізика",
                scientificValue: "Висока",
                recentDiscoveries: Discoveries[random.Next(Discoveries.Length)]
            );
        }

        private static FashionMagazine GenerateRandomFashionMagazine()
        {
            return new FashionMagazine(
                name: $"Мода {Names[random.Next(Names.Length)]}",
                price: random.Next(100, 300),
                periodicity: Periodicities[random.Next(Periodicities.Length)],
                publisher: Publishers[random.Next(Publishers.Length)],
                topic: "Мода",
                targetAudience: random.Next(0, 2) == 0 ? "Жінки 18-35" : "Чоловіки 20-40",
                trends: Trends[random.Next(Trends.Length)]
            );
        }
    }
}
