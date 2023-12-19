namespace AdventOfCode.Y2015.Solvers
{
    public class Day21 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var boss = ToBoss(input);
            var costs = new List<int>();
            foreach (var (cost, damage, armor) in GetCombinations())
            {
                var player = new Creature(100, damage, armor);
                if (PlayerWinsMatch(player, boss.Duplicate()))
                {
                    costs.Add(cost);
                }
            }
            return costs.Min();
        }

        public override object SolvePart2(string[] input)
        {
            var boss = ToBoss(input);
            var costs = new List<int>();
            foreach (var (cost, damage, armor) in GetCombinations())
            {
                var player = new Creature(100, damage, armor);
                if (!PlayerWinsMatch(player, boss.Duplicate()))
                {
                    costs.Add(cost);
                }
            }
            return costs.Max();
        }

        private static List<(int Cost, int Damage, int Armor)> GetCombinations()
        {
            var combinations = new List<(int Cost, int Damage, int Armor)>();
            var weapons = GetWeapons();
            var armors = GetArmors();
            var rings = GetRings();
            foreach (var weapon in weapons)
            {
                foreach (var armor in armors)
                {
                    for (int ring1 = 0; ring1 < rings.Length; ring1++)
                    {
                        for (int ring2 = 0; ring2 < rings.Length; ring2++)
                        {
                            if (ring1 == ring2)
                            {
                                continue;
                            }
                            var cost = weapon.Cost + armor.Cost + rings[ring1].Cost + rings[ring2].Cost;
                            var damage = weapon.Damage + rings[ring1].Damage + rings[ring2].Damage;
                            var armorPoints = armor.Armor + rings[ring1].Armor + rings[ring2].Armor;
                            combinations.Add((cost, damage, armorPoints));
                        }
                    }
                }
            }
            return combinations;
        }

        private static bool PlayerWinsMatch(Creature player, Creature boss)
        {
            var creatures = new[] { player, boss };
            var current = 0;
            while (creatures.All(creature => creature.HitPoints > 0))
            {
                Attack(creatures[current], creatures[++current % 2]);
                current %= 2;
            }
            return player.HitPoints > 0;
        }

        private static void Attack(Creature attacker, Creature defender) => defender.HitPoints -= Math.Max(1, attacker.Damage - defender.Armor);
        private static Creature ToBoss(string[] lines) => new(int.Parse(lines[0].Split(' ')[^1]), int.Parse(lines[1].Split(' ')[^1]), int.Parse(lines[2].Split(' ')[^1]));

        private static Item[] GetWeapons()
        {
            return
            [
                new(ItemType.Weapon, 8, 4, 0),
                new(ItemType.Weapon, 10, 5, 0),
                new(ItemType.Weapon, 25, 6, 0),
                new(ItemType.Weapon, 40, 7, 0),
                new(ItemType.Weapon, 74, 8, 0)
            ];
        }

        private static Item[] GetArmors()
        {
            return
            [
                new(ItemType.Armor, 0, 0, 0),
                new(ItemType.Armor, 13, 0, 1),
                new(ItemType.Armor, 31, 0, 2),
                new(ItemType.Armor, 53, 0, 3),
                new(ItemType.Armor, 75, 0, 4),
                new(ItemType.Armor, 102, 0, 5)
            ];
        }

        private static Item[] GetRings()
        {
            return
            [
                new(ItemType.Ring, 0, 0, 0),
                new(ItemType.Ring, 0, 0, 0),
                new(ItemType.Ring, 25, 1, 0),
                new(ItemType.Ring, 50, 2, 0),
                new(ItemType.Ring, 100, 3, 0),
                new(ItemType.Ring, 20, 0, 1),
                new(ItemType.Ring, 40, 0, 2),
                new(ItemType.Ring, 80, 0, 3)
            ];
        }

        private enum ItemType { Weapon, Armor, Ring }
        private readonly record struct Item(ItemType ItemType, int Cost, int Damage, int Armor);

        private record class Creature()
        {
            public int HitPoints { get; set; }
            public int Damage { get; init; }
            public int Armor { get; init; }
            public Creature(int hitPoints, int damage, int armor) : this()
            {
                HitPoints = hitPoints;
                Damage = damage;
                Armor = armor;
            }
            public Creature Duplicate()
            {
                return new Creature(HitPoints, Damage, Armor);
            }
        }
    }
}
