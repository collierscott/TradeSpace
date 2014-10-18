using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Environment
{
    public static partial class Env
    {
        private static readonly List<Goods> GoodsList = new List<Goods>
        {
            #region Minerals

            new Goods { Id = GoodsId.Ferrum, Type = GoodsType.Minerals, TechLevel = 1, Mass = 8, Volume = 2, Price = 20,
                Name = "Феррум", Description = "Ковкий металл серебристо-белого цвета с высокой химической реакционной способностью. На него приходится до 95 % мирового металлургического производства." },
            new Goods { Id = GoodsId.Titanium, Type = GoodsType.Minerals, TechLevel = 1, Mass = 4, Volume = 2, Price = 20,
                Name = "Титаниум", Description = "Лёгкий прочный металл серебристо-белого цвета, используемый в качестве конструкционных материалов. Сложность технологического процесса обработки приводит к значительному росту цен на конечный продукт." },
            new Goods { Id = GoodsId.Chromium, Type = GoodsType.Minerals, TechLevel = 1, Mass = 7, Volume = 2, Price = 20,
                Name = "Хромиум", Description = "Твёрдый металл голубовато-белого цвета. Хром применяется для производства сплавов, а также износоустойчивых и красивых гальванических покрытий." },
            new Goods { Id = GoodsId.Wolframium, Type = GoodsType.Minerals, TechLevel = 1, Mass = 19, Volume = 2, Price = 20,
                Name = "Волфрамиум", Description = "При нормальных условиях представляет собой твёрдый блестящий серебристо-серый переходный металл. Вольфрам — самый тугоплавкий из металлов. Благодаря высокой плотности является основой тяжёлых сплавов." },
            new Goods { Id = GoodsId.Platinum, Type = GoodsType.Minerals, TechLevel = 1, Mass = 21, Volume = 2, Price = 20,
                Name = "Платинум", Description = "Блестящий благородный металл серебристо-белого цвета. Платина применяется в качестве легирующей добавки для производства высокопрочных сталей, в ювелирном деле и медицине." },
            new Goods { Id = GoodsId.Niccolum, Type = GoodsType.Minerals, TechLevel = 1, Mass = 9, Volume = 2, Price = 20,
                Name = "Николум", Description = "Пластичный, ковкий, переходный металл серебристо-белого цвета, при обычных температурах на воздухе покрывается тонкой плёнкой оксида. Никель является основой большинства суперсплавов — жаропрочных материалов, применяемых в аэрокосмической промышленности." },
            new Goods { Id = GoodsId.Cobaltum, Type = GoodsType.Minerals, TechLevel = 1, Mass = 9, Volume = 2, Price = 20,
                Name = "Кобальтум", Description = "Серебристо-белый, слегка желтоватый металл с розоватым или синеватым отливом. Кобальтат применяется для изготовления постоянных магнитов, используемых в качестве сердечников электромоторов и трансформаторов. Сплавы с применением кобальта применяют для изготовления горнодобывающего оборудования." },
            new Goods { Id = GoodsId.Argentum, Type = GoodsType.Minerals, TechLevel = 1, Mass = 10, Volume = 2, Price = 20,
                Name = "Аргентум", Description = "Ковкий, пластичный благородный металл серебристо-белого цвета. Серебро широко применяется в составе сплавов, для изготовления медицинского оборудования, а также в ювелирном деле." },
            new Goods { Id = GoodsId.Aurum, Type = GoodsType.Minerals, TechLevel = 1, Mass = 19, Volume = 2, Price = 20,
                Name = "Аурум", Description = "Благородный металл жёлтого цвета. Золото широко используется в ювелирном деле и медицине, а также в микроэлектронике при производстве проводников, контактных поверхностей и печатных плат." },
            new Goods { Id = GoodsId.Uranium, Type = GoodsType.Minerals, TechLevel = 1, Mass = 19, Volume = 2, Price = 20,
                Name = "Ураниум", Description = "Тяжёлый, серебристо-белый глянцеватый металл. Наибольшее применение имеет изотоп урана, который используется как топливо в ядерных реакторах. Уран также нашел применение в геологии для определение возраста минералов и горных пород. Добыча и торговля ураном в коммерческих целях без соответствующего разрешения запрещена." },
            new Goods { Id = GoodsId.Polonium, Type = GoodsType.Minerals, TechLevel = 1, Mass = 9, Volume = 2, Price = 20,
                Name = "Полониум", Description = "При нормальных условиях представляет собой мягкий металл серебристо-белого цвета. Полоний применяется для изготовления компактных и очень мощных нейтронных источников." },
            new Goods { Id = GoodsId.Plutonium, Type = GoodsType.Minerals, TechLevel = 1, Mass = 20, Volume = 2, Price = 20,
                Name = "Плутониум", Description = "Тяжёлый хрупкий радиоактивный металл серебристо-белого цвета. Плутоний используется в элементах питания в качестве ядерного топлива." },
            new Goods { Id = GoodsId.Selenium, Type = GoodsType.Minerals, TechLevel = 1, Mass = 4, Volume = 2, Price = 20,
                Name = "Селениум", Description = "Хрупкий, блестящий на изломе неметалл чёрного цвета. Используется в медицине как мощное противораковое средство, а также для профилактики широкого спектра заболеваний." },    
            new Goods { Id = GoodsId.Chalcanthite, Type = GoodsType.Minerals, TechLevel = 1, Mass = 2, Volume = 2, Price = 20,
                Name = "Халькантит", Description = "Соблазнительные синие кристаллы халькантита состоят из меди, в сочетании с серой и другими элементами, а также с водой. Используется исключительно в декоративных целях." },
            new Goods { Id = GoodsId.Torbernite, Type = GoodsType.Minerals, TechLevel = 1, Mass = 3, Volume = 2, Price = 20,
                Name = "Торбенит", Description = "Зелёные кристаллы в форме призмы образуются в качестве вторичных месторождений в гранитных скалах и состоят из урана. Ярко-зеленые скопления кристаллов используются в качестве индикаторов урановых месторождений." },
            new Goods { Id = GoodsId.Cinnabar, Type = GoodsType.Minerals, TechLevel = 1, Mass = 8, Volume = 2, Price = 20,
                Name = "Киноварь", Description = "Чрезвычайно токсичный минерал темно-красного цвета. Название кристалла означает «кровь дракона». Используется для добычи ртути." },
            new Goods { Id = GoodsId.Diamond, Type = GoodsType.Minerals, TechLevel = 1, Mass = 3, Volume = 2, Price = 20,
                Name = "Алмаз", Description = "Обладающий высочайшей прочностью прозрачный кристалл. В природе встречается крайне редко и бывает раличных цветов. Применяется в основном в ювелирном деле. В промышленности практически полностью заменен адамантом." },
            new Goods { Id = GoodsId.Union, Type = GoodsType.Minerals, TechLevel = 1, Mass = 7, Volume = 2, Price = 20,
                Name = "Юнион", Description = "Ковкий металл темно-зеленого цвета. По свойствам схож с феррумом и в прошлом считался его изотопом. Юнион более твердый, но при этому имеет меньший удельный вес. Кроме того он и не подвержен коррозии. После обнаружения колоссальных залежей в системе Union поступил на глобальный рынок и нашел широкое применение в промышленности, в том числе в качестве замены феррума." },
            new Goods { Id = GoodsId.Dragonite, Type = GoodsType.Minerals, TechLevel = 1, Mass = 12, Volume = 2, Price = 20,
                Name = "Драгонит", Description = "Невероятно прочный тяжелый металл темно-красного цвета. Крайне редко встечается в природе, поэтому найти его в свободной продаже можно далеко не всегда. Цена на драгонит на порядок выше любой альтернатив. Тем не менее, драгонит активно применяется в специализированном производстве прототипов самого современного оборудования." },
            new Goods { Id = GoodsId.Adamant, Type = GoodsType.Minerals, TechLevel = 1, Mass = 4, Volume = 2, Price = 20,
                Name = "Адамант", Description = "Кристалл темно-фиолетового цвета. Незначительно прочнее алмаза, однако ввиду обнаружения крупных местарождений перестал быть драгоценным. Широко используется промышленности, в том числе для синтеза более прочных искусственных кристаллов." },
            new Goods { Id = GoodsId.S1800, Type = GoodsType.Minerals, TechLevel = 1, Mass = 2, Volume = 2, Price = 20,
                Name = "Кристалл S1800", Description = "Искусственный кристалл, имеющий абсолютную твердость 1800." },
            new Goods { Id = GoodsId.S1900, Type = GoodsType.Minerals, TechLevel = 1, Mass = 2, Volume = 2, Price = 20,
                Name = "Кристалл S1900", Description = "Искусственный кристалл, имеющий абсолютную твердость 1900." },
            new Goods { Id = GoodsId.S1950, Type = GoodsType.Minerals, TechLevel = 1, Mass = 3, Volume = 2, Price = 20,
                Name = "Кристалл S1950", Description = "Искусственный кристалл, имеющий абсолютную твердость 1950." },
            new Goods { Id = GoodsId.Lightcrystal, Type = GoodsType.Minerals, TechLevel = 1, Mass = 2, Volume = 2, Price = 20,
                Name = "Лайткристал", Description = "Чрезвычайно твердый кристалл, при определенных условиях имеющий бледно-белое свечение. Используется в горнодобывающей промышленности." },
            new Goods { Id = GoodsId.Novacrystal, Type = GoodsType.Minerals, TechLevel = 1, Mass = 1, Volume = 2, Price = 20,
                Name = "Новакристал", Description = "Очень редкий и безумно дорогой кристалл, имеющий постоянное ярко-желтое свечение. Используется исключительно в декоративных целях." },
            new Goods { Id = GoodsId.Oil, Type = GoodsType.Minerals, TechLevel = 1, Mass = 3, Volume = 2, Price = 20,
                Name = "Нефть", Description = "природная маслянистая горючая жидкость со специфическим запахом, состоящая в основном из сложной смеси углеводородов различной молекулярной массы и некоторых других химических соединений." },  
            
            #endregion

            #region Substance
            
            new Goods { Id = GoodsId.Catalyst, Type = GoodsType.Substance, TechLevel = 0,
                Name = "Катализатор", Description = "", Mass = 0, Volume = 0, Price = 0 },

            #endregion

            #region Food

            new Goods { Id = GoodsId.Water, Type = GoodsType.Food, TechLevel = 0,
                Name = "Питевая вода", Description = "", Mass = 1, Volume = 4, Price = 20 },
            new Goods { Id = GoodsId.Grain, Type = GoodsType.Food, TechLevel = 0,
                Name = "Зерно", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.Fish, Type = GoodsType.Food, TechLevel = 0,
                Name = "Рыба", Description = "", Mass = 2, Volume = 4, Price = 50 },
            new Goods { Id = GoodsId.Meat, Type = GoodsType.Food, TechLevel = 0,
                Name = "Мясо", Description = "", Mass = 0, Volume = 0, Price = 0 },
            
            #endregion

            #region Materials

            new Goods { Id = GoodsId.Leather, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Кожа", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.Fur, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Мех", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.Wool, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Шерсть", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.Silk, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Шелк", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.Paper, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Бумага", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.Lumber, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Пиломатериалы", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.ConstructionMaterials, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Стройматериалы", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.ReinforcedConcrete, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Железобетон", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.SynteticFiber, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Синтетические волокна", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.Plastics, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Пластик", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.Carbon, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Карбон", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.Fiberglass, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Фибергласовые волокна", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.CompositeAlloys, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Композитные сплавы", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.Ceramics, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Керамика", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.Glass, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Стекло", Description = "", Mass = 0, Volume = 0, Price = 0 },

            #endregion

            #region Clothes

            new Goods { Id = GoodsId.Boots, Type = GoodsType.Clothes, TechLevel = 0,
                Name = "Ботинки", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.Jeans, Type = GoodsType.Clothes, TechLevel = 0,
                Name = "Джинсы", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.Coveralls, Type = GoodsType.Clothes, TechLevel = 0,
                Name = "Спецодежда", Description = "", Mass = 0, Volume = 0, Price = 0 },
            new Goods { Id = GoodsId.LeatherJacket, Type = GoodsType.Clothes, TechLevel = 0,
                Name = "Кожаные куртки", Description = "", Mass = 0, Volume = 0, Price = 0 },

            #endregion

            #region House

			new Goods { Id = GoodsId.PlasticWare, Type = GoodsType.House, TechLevel = 0,
                Name = "Пластиковая посуда", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Porcelain, Type = GoodsType.House, TechLevel = 0,
                Name = "Фарфор", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Furniture, Type = GoodsType.House, TechLevel = 0,
                Name = "Мебель", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Carpet, Type = GoodsType.House, TechLevel = 0,
                Name = "Ковры", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Plumbing, Type = GoodsType.House, TechLevel = 0,
                Name = "Сантехника", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.WallScreen, Type = GoodsType.House, TechLevel = 0,
                Name = "Настенный экран", Description = "", Mass = 0, Volume = 0, Price = 0 },

            #endregion

            #region Health

            new Goods { Id = GoodsId.Shampoo, Type = GoodsType.Health, TechLevel = 0,
                Name = "Шампунь", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Cologne, Type = GoodsType.Health, TechLevel = 0,
                Name = "Одеколон", Description = "", Mass = 0, Volume = 0, Price = 0 },

            #endregion

            #region Medicines

			new Goods { Id = GoodsId.FirstAidKit, Type = GoodsType.Medicines, TechLevel = 0,
                Name = "Аптечка", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Antibiotics, Type = GoodsType.Medicines, TechLevel = 0,
                Name = "Антиботики", Description = "", Mass = 0, Volume = 0, Price = 0 },

            #endregion

            #region Entertainment

			new Goods { Id = GoodsId.Toys, Type = GoodsType.Entertainment, TechLevel = 0,
                Name = "Игрушки", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.PlayStation, Type = GoodsType.Entertainment, TechLevel = 0,
                Name = "Игровые приставки", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.PortableCinema, Type = GoodsType.Entertainment, TechLevel = 0,
                Name = "Портативный кинотеатр", Description = "", Mass = 0, Volume = 0, Price = 0 },

            #endregion

            #region Sports

            new Goods { Id = GoodsId.Hoverboard, Type = GoodsType.Sports, TechLevel = 0,
                Name = "Ховерборд", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Gravitron, Type = GoodsType.Sports, TechLevel = 0,
                Name = "Гравитрон", Description = "", Mass = 0, Volume = 0, Price = 0 },

            #endregion

            #region Tobacco

			new Goods { Id = GoodsId.Cigarettes, Type = GoodsType.Tobacco, TechLevel = 0,
                Name = "Сигареты", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Cigar, Type = GoodsType.Tobacco, TechLevel = 0,
                Name = "Сигары", Description = "", Mass = 0, Volume = 0, Price = 0 },

            #endregion

            #region Alcohol

    		new Goods { Id = GoodsId.Vine, Type = GoodsType.Alcohol, TechLevel = 0,
                Name = "Вино", Description = "", Mass = 0, Volume = 0, Price = 0 },
		    new Goods { Id = GoodsId.Whiskey, Type = GoodsType.Alcohol, TechLevel = 0,
                Name = "Виски", Description = "", Mass = 0, Volume = 0, Price = 0 },
		    new Goods { Id = GoodsId.Vodka, Type = GoodsType.Alcohol, TechLevel = 0,
                Name = "Водка", Description = "", Mass = 0, Volume = 0, Price = 0 },

            #endregion

            #region Sex

			new Goods { Id = GoodsId.Vibrator, Type = GoodsType.Sex, TechLevel = 0,
                Name = "Вибраторы", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.PleasureHelm, Type = GoodsType.Sex, TechLevel = 0,
                Name = "Шлемы удовольствий", Description = "", Mass = 0, Volume = 0, Price = 0 },

            #endregion

            #region Hardware

			new Goods { Id = GoodsId.Diode, Type = GoodsType.Hardware, TechLevel = 0,
                Name = "Диоды", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Semiconductor, Type = GoodsType.Hardware, TechLevel = 0,
                Name = "Проводники", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.PrintedCircuitBoard, Type = GoodsType.Hardware, TechLevel = 0,
                Name = "Печатные платы", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Processor, Type = GoodsType.Hardware, TechLevel = 0,
                Name = "Процессоры", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.ByteStorage, Type = GoodsType.Hardware, TechLevel = 0,
                Name = "Байт-хранилища", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Led, Type = GoodsType.Hardware, TechLevel = 0,
                Name = "Дисплеи", Description = "", Mass = 0, Volume = 0, Price = 0 },

            #endregion

            #region Electronics

            new Goods { Id = GoodsId.Smartphone, Type = GoodsType.Electronics, TechLevel = 0,
                Name = "Смартфоны", Description = "", Mass = 1, Volume = 2, Price = 100 },
			new Goods { Id = GoodsId.SmartTv, Type = GoodsType.Electronics, TechLevel = 0,
                Name = "SmartTV", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.VacuumCleaner, Type = GoodsType.Electronics, TechLevel = 0,
                Name = "Дроиды-пылесосы", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.HandyToilet, Type = GoodsType.Electronics, TechLevel = 0,
                Name = "Портативный туалет", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Refrigerator, Type = GoodsType.Electronics, TechLevel = 0,
                Name = "Холодильники", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Heater, Type = GoodsType.Electronics, TechLevel = 0,
                Name = "Обогреватели", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.AirConditioning, Type = GoodsType.Electronics, TechLevel = 0,
                Name = "Кондиционеры", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Fan, Type = GoodsType.Electronics, TechLevel = 0,
                Name = "Вентиляторы", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.Spotlight, Type = GoodsType.Electronics, TechLevel = 0,
                Name = "Фонари", Description = "", Mass = 0, Volume = 0, Price = 0 },

            #endregion

            #region Nature

			new Goods { Id = GoodsId.Pets, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Домашние животные", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.ExoticPlants, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Экзотические растения", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.ExoticAnimals, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Экзотические животные", Description = "", Mass = 0, Volume = 0, Price = 0 },
			new Goods { Id = GoodsId.ExoticFish, Type = GoodsType.Materials, TechLevel = 0,
                Name = "Экзотические рыбы", Description = "", Mass = 0, Volume = 0, Price = 0 },

            #endregion
            
        };

        public static readonly Dictionary<GoodsId, Goods> GoodsDatabase = GoodsList.ToDictionary(i => i.Id);
    }
}