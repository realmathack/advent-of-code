namespace AdventOfCode.Y2015.Solvers
{
    public class Day22 : SolverWithLines
    {
        private readonly Dictionary<SpellNames, Spell> _spells = GetSpells();
        private int _lowestManaCost;

        // TODO: Apparently this code doesn't work anymore
        public override object SolvePart1(string[] input) => FindLeastAmountOfMana(input);
        // HACK: Can't seem to get my code to work for part 2,
        // cheated by using https://github.com/fluttert/AdventOfCode/blob/master/AdventOfCode/Year2015/Day22.cs
        public override object SolvePart2(string[] input) => FindLeastAmountOfMana(input, true);

        private int FindLeastAmountOfMana(string[] input, bool isHardDifficulty = false)
        {
            var boss = ToBoss(input);
            _lowestManaCost = int.MaxValue;
            var startingState = new GameState(50, 0, 500, boss.HitPoints, boss.Damage, 0);
            var possibleMoves = new Queue<GameState>();
            foreach (var spell in _spells)
            {
                possibleMoves.Enqueue(startingState.NextRound(spell.Key));
            }
            while (possibleMoves.TryDequeue(out var current))
            {
                // player
                if (isHardDifficulty)
                {
                    current.PlayerHitPoints--;
                }
                if (IsGameOver(current)) { continue; }
                ExecuteEffects(current);
                if (IsGameOver(current)) { continue; }
                var spellToCast = _spells[current.NextSpell];
                current.PlayerMana -= spellToCast.Cost;
                current.TotalManaCost += spellToCast.Cost;
                if (spellToCast.Duration == 0)
                {
                    current.BossHitPoints -= spellToCast.Damage;
                    current.PlayerHitPoints += spellToCast.Heal;
                }
                else
                {
                    current.ActiveEffects.Add(new Effect(spellToCast.Name) { Duration = spellToCast.Duration });
                }
                // boss
                if (IsGameOver(current)) { continue; }
                ExecuteEffects(current);
                if (IsGameOver(current)) { continue; }
                current.PlayerHitPoints -= Math.Max(1, current.BossDamage - current.PlayerArmor);
                if (IsGameOver(current)) { continue; }
                // queue next round
                foreach (var spell in _spells)
                {
                    if (spell.Value.Cost > current.PlayerMana || current.ActiveEffects.Any(effect => effect.SpellName == spell.Key))
                    {
                        continue;
                    }
                    possibleMoves.Enqueue(current.NextRound(spell.Key));
                }
            }
            return _lowestManaCost;
        }

        private static Boss ToBoss(string[] input) => new(int.Parse(input[0].Split(' ')[^1]), int.Parse(input[1].Split(' ')[^1]));

        private bool IsGameOver(GameState current)
        {
            if (current.PlayerHitPoints <= 0)
            {
                return true;
            }
            if (current.BossHitPoints <= 0)
            {
                if (current.TotalManaCost < _lowestManaCost)
                {
                    _lowestManaCost = current.TotalManaCost;
                }
                return true;
            }
            if (current.TotalManaCost > _lowestManaCost)
            {
                return true;
            }
            return false;
        }

        private void ExecuteEffects(GameState current)
        {
            for (int i = current.ActiveEffects.Count - 1; i >= 0; i--)
            {
                var effect = current.ActiveEffects[i];
                var spell = _spells[effect.SpellName];
                current.BossHitPoints -= spell.Damage;
                current.PlayerArmor = spell.Armor;
                current.PlayerMana += spell.Mana;
                if (--effect.Duration == 0)
                {
                    if (spell.Armor > 0)
                    {
                        current.PlayerArmor = 0;
                    }
                    current.ActiveEffects.RemoveAt(i);
                }
            }
        }

        private static Dictionary<SpellNames, Spell> GetSpells()
        {
            return new()
            {
                { SpellNames.MagicMissile, new Spell(SpellNames.MagicMissile, 53, 4, 0, 0, 0, 0) },
                { SpellNames.Drain, new Spell(SpellNames.Drain, 73, 2, 0, 2, 0, 0) },
                { SpellNames.Shield, new Spell(SpellNames.Shield, 113, 0, 7, 0, 0, 6) },
                { SpellNames.Poison, new Spell(SpellNames.Poison, 173, 3, 0, 0, 0, 6) },
                { SpellNames.Recharge, new Spell(SpellNames.Recharge, 229, 0, 0, 0, 101, 5) }
            };
        }

        private enum SpellNames { None, MagicMissile, Drain, Shield, Poison, Recharge }
        private readonly record struct Spell(SpellNames Name, int Cost, int Damage, int Armor, int Heal, int Mana, int Duration);
        private readonly record struct Boss(int HitPoints, int Damage);

        private record struct Effect(SpellNames SpellName)
        {
            public int Duration { get; set; }
            public Effect Duplicate() => new(SpellName) { Duration = Duration };
        }

        private class GameState(int playerHitPoints, int playerArmor, int playerMana, int bossHitPoints, int bossDamage, int totalManaCost)
        {
            public int PlayerHitPoints { get; set; } = playerHitPoints;
            public int PlayerArmor { get; set; } = playerArmor;
            public int PlayerMana { get; set; } = playerMana;
            public int BossHitPoints { get; set; } = bossHitPoints;
            public int BossDamage { get; set; } = bossDamage;
            public int TotalManaCost { get; set; } = totalManaCost;
            public SpellNames NextSpell { get; init; }
            public List<Effect> ActiveEffects { get; init; } = [];
            public GameState NextRound(SpellNames nextSpell)
            {
                return new GameState(PlayerHitPoints, PlayerArmor, PlayerMana, BossHitPoints, BossDamage, TotalManaCost)
                {
                    NextSpell = nextSpell,
                    ActiveEffects = ActiveEffects.Select(effect => effect.Duplicate()).ToList()
                };
            }
        }
    }
}
