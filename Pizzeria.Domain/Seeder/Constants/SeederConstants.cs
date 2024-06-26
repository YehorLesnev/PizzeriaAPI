﻿namespace Pizzeria.Domain.Seeder.Constants
{
    public static class SeederConstants
    {
        public static readonly DateTime OrdersStartDate = new(2024, 1, 1, 8, 0, 0);

        public const int NumOfStaff = 70;
        public const int MinNumOfCustomers = 100;
        public const int MaxNumOfCustomers = 250;

        public const string ImageExtension = "webp";

        public static List<string> IngredientNames =
        [
            "flour",
            "bechamel sauce",
            "parmesan cheese",
            "mozzarella cheese",
            "dorblu cheese",
            "mozzarella mini",
            "walnut",
            "pear",
            "sweet and sour sauce",
            "chicken",
            "onions",
            "banana",
            "bell pepper",
            "garlic powder",
            "paprika",
            "celery",
            "chives",
            "cilantro",
            "cinnamon",
            "cloves",
            "coconut milk",
            "curry powder",
            "dill",
            "fennel",
            "ginger",
            "honey",
            "ketchup",
            "leeks",
            "lemongrass",
            "licorice",
            "mustard seeds",
            "nutmeg",
            "oats",
            "pasta",
            "peanuts",
            "pesto",
            "pita bread",
            "poppy seeds",
            "quinoa",
            "radish",
            "rhubarb",
            "rosemary",
            "sage",
            "salsa",
            "sesame seeds",
            "sorrel",
            "soy sauce",
            "spinach",
            "sprouts",
            "star anise",
            "tamarind",
            "thyme",
            "tofu",
            "tortilla",
            "truffle",
            "vanilla",
            "vinegar",
            "wasabi",
            "watercress",
            "wheat germ",
            "yeast",
            "zucchini",
            "arrowroot",
            "artichoke",
            "basil",
            "bok choy",
            "broccoli",
            "brussels sprouts",
            "cabbage",
            "caraway seeds",
            "cardamom",
            "cashews",
            "cayenne pepper",
            "chia seeds",
            "chili flakes",
            "chives",
            "coconut oil",
            "coriander",
            "cranberries",
            "cucumber",
            "dates",
            "dijon mustard",
            "endive",
            "escarole",
            "feta cheese",
            "figs",
            "flaxseed",
            "fennel seeds",
            "garlic",
            "garam masala",
            "hazelnuts",
            "jicama",
            "kale",
            "kiwi",
            "lentils",
            "lima beans",
            "mango",
            "miso",
            "molasses",
            "nutritional yeast",
            "olives",
            "orange zest",
            "oyster sauce",
            "parsley",
            "pepper",
            "pine nuts",
            "pomegranate",
            "portobello mushrooms",
            "quince",
            "ramps",
            "raspberries",
            "rutabaga",
            "saffron",
            "shallots",
            "sherry",
            "sour cream",
            "soy milk",
            "sunflower seeds",
            "tahini",
            "tamari",
            "tempeh",
            "tequila",
            "tumeric",
            "wasabi",
            "water chestnuts",
            "wheat berries",
            "yams",
            "zatar"
        ];

        public static List<string> ItemSizeNames =
            [
                "S",
                "M",
                "L",
                "XL"
            ];

        public static List<string> PizzaItemNames =
        [
            "Quattro Formaggi",
            "Sicilian pizza",
            "California Pizza",
            "Chicago pizza",
            "Capricciosa",
            "Veggie pizza",
            "BBQ chicken pizza",
            "Pizzette",
            "Hawaiian pizza",
            "Greek Pizza",
            "Margarita",
            "Pepperoni pizza",
            "Prosciutto",
            "Alla vongole",
            "Calzone",
            "Detroit Pizza",
            "French Break Pizza",
            "Brick Oven Pizza",
            "Angelina Pizza",
            "Salami Pizza",
            "Burger Pizza",
            "Bavarian Pizza",
            "Benefika Pizza",
            "Pancetta Pizza",
            "Chipolla Pizza",
            "Diabola Pizza",
            "Parma Pizza",
            "Schinkarella Pizza",
            "Formajipesto Pizza",
            "Polo Pizza",
            "Tonno Pizza",
            "Florentyna Pizza"
        ];

        public static List<string> BurgerItemNames =
        [
            "Big Father",
            "BFG Burger",
            "Sweet Burger",
            "Hot Mama",
            "Camembert",
            "Normal",
            "Cheeseburger",
            "Chicken",
            "Nuggets"
        ];

        public static List<string> FrenchFriesItemNames =
        [
            "French fries",
            "French fries with dopa",
            "Dips",
            "Home-made potato",
            "Baked potato",
            "Sweet potatoes with tartar sauce",
            "Sweet potatoes",
            "Potato croquettes",
            "Mashed potato"
        ];

        public static List<string> HotDogItemNames =
        [
            "Branded",
            "German",
            "Bavarian",
            "American",
            "Italian",
            "Classic",
            "Double",
            "Long One"
        ];


        public static List<string> StaffPositions =
        [
            "Runner",
            "Junior Waiter",
            "Waiter",
            "Senior Waiter",
            "Junior Cashier",
            "Cashier",
            "Senior Cashier",
            "Manager",
            "Cook",
            "Pizzaiola",
            "Chef",
        ];

        public static List<decimal> StaffPayRates =
        [
            6.49m,
            8.00m,
            10.00m,
            12.00m,
            8.99m,
            9.49m,
            12.00m,
            17.00m,
            9.00m,
            11.00m,
            15.00m,
        ];
    }
}