﻿using ConsoleStore.Categories.Models;
using ConsoleStore.Products.Models;

namespace ConsoleStore.Common.Models;

static class TestProducts
{
    public static List<Category> testCategories = new List<Category>()
    {
        new Category("Электроника")
        {
            Products =
            {
                new Product("Телевизор", "Погрузитесь в мир развлечений с этим ультратонким телевизором, который делает каждую деталь ярче и четче.", 24000, 4),
                new Product("Чайник", "Этот стильный чайник быстро вскипятит воду, чтобы вы могли насладиться любимым чаем в считанные минуты.", 3000, 3),
                new Product("Наушники", "Погрузитесь в мир музыки с этими наушниками, которые обеспечивают кристально чистый звук и комфорт на весь день.", 7000, 12)
            }
        },
        new Category("Одежда")
        {
            Products =
            {
                new Product("Футболка", "Легкая и дышащая футболка, которая идеально подходит как для активного отдыха, так и для повседневной носки.", 1500, 10),
                new Product("Джинсы", "Классические джинсы с идеальной посадкой, которые подчеркнут вашу индивидуальность и стиль.", 3000, 5),
                new Product("Куртка", "Эта стильная куртка защитит вас от непогоды и станет вашим верным спутником в холодные дни.", 5000, 2),
                new Product("Платье", "Элегантное платье, которое идеально подойдет для романтического ужина или вечеринки с друзьями.", 4000, 8),
                new Product("Кроссовки", "Удобные кроссовки для активных людей, которые ценят стиль и комфорт в каждом шаге.", 4500, 6)
            }
        },
        new Category("Посуда")
        {
            Products =
            {
                new Product("Сковорода", "Эта антипригарная сковорода позволит вам готовить с легкостью и удовольствием, не беспокоясь о пригорании.", 2000, 15),
                new Product("Ножи", "Набор профессиональных ножей, который сделает процесс нарезки быстрым и легким — идеальный помощник на кухне.", 1200, 20),
                new Product("Чашки", "Стильные чашки с уникальным дизайном, которые добавят уют в каждую вашу утреннюю кофе-паузу.", 800, 25),
                new Product("Тарелки", "Эти изысканные тарелки станут настоящим украшением вашего стола и подчеркнут каждое блюдо.", 1000, 18),
                new Product("Кастрюля", "Прочная кастрюля с толстым дном, которая равномерно распределяет тепло и позволяет готовить на высшем уровне.", 3500, 5)
            }
        },
        new Category("Кондитерские изделия")
        {
            Products =
            {
                new Product("Шоколадный торт", "Нежный шоколадный торт, пропитанный ароматным сиропом, станет настоящим украшением любого праздника.", 450, 5),
                new Product("Песочное печенье", "Хрустящее песочное печенье с добавлением ванили — идеальное лакомство к чаю или кофе, которое приятно удивит своим вкусом.", 300, 20),
                new Product("Кремовые пирожные", "Эти легкие кремовые пирожные с фруктовой начинкой подарят вам незабываемые моменты наслаждения и сладости.", 1200, 15),
                new Product("Трюфели", "Шоколадные трюфели с нежной начинкой, которые растопят ваше сердце и подарят моменты истинного блаженства.", 180, 8)
            }
        }
    };
}
