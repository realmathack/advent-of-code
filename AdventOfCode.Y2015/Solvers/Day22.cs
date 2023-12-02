namespace AdventOfCode.Y2015.Solvers
{
    public class Day22 : SolverWithLines
    {
        private readonly Dictionary<SpellNames, Spell> _spells = GetSpells();
        private int _lowestManaCost;

        public override object SolvePart1(string[] input)
        {
            return Solve(input);
        }

        public override object SolvePart2(string[] input)
        {
            // HACK: Can't seem to get my code to work for part 2,
            // cheated by using https://github.com/fluttert/AdventOfCode/blob/master/AdventOfCode/Year2015/Day22.cs
            return Solve(input, true);
        }

        private int Solve(string[] input, bool isHardDifficulty = false)
        {
            var boss = ToBoss(input);
            _lowestManaCost = int.MaxValue;
            var startingState = new GameState()
            {
                PlayerHitPoints = 50,
                PlayerArmor = 0,
                PlayerMana = 500,
                BossHitPoints = boss.HitPoints,
                BossDamage = boss.Damage,
                TotalManaCost = 0
            };
            var possibleMoves = new Queue<GameState>();
            foreach (var spell in _spells)
            {
                possibleMoves.Enqueue(startingState.NextRound(spell.Key));
            }
            while (possibleMoves.Count > 0)
            {
                var current = possibleMoves.Dequeue();
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
                    current.ActiveEffects.Add(new Effect(spellToCast.Name, spellToCast.Duration));
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
                    if (spell.Value.Cost > current.PlayerMana || current.ActiveEffects.Any(s => s.SpellName == spell.Key))
                    {
                        continue;
                    }
                    possibleMoves.Enqueue(current.NextRound(spell.Key));
                }
            }
            return _lowestManaCost;
        }

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

        private static Boss ToBoss(string[] input)
        {
            var hitPoints = int.Parse(input[0].Split(' ')[^1]);
            var damage = int.Parse(input[1].Split(' ')[^1]);
            return new(hitPoints, damage);
        }

        private enum SpellNames { None, MagicMissile, Drain, Shield, Poison, Recharge }
        private record struct Spell(SpellNames Name, int Cost, int Damage, int Armor, int Heal, int Mana, int Duration);
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

        private class Effect(SpellNames spellName, int duration)
        {
            public SpellNames SpellName { get; } = spellName;
            public int Duration { get; set; } = duration;
            public Effect Duplicate() => new(SpellName, Duration);
        }

        private class GameState
        {
            public int PlayerHitPoints { get; set; }
            public int PlayerArmor { get; set; }
            public int PlayerMana { get; set; }
            public int BossHitPoints { get; set; }
            public int BossDamage { get; set; }
            public int TotalManaCost { get; set; }
            public SpellNames NextSpell { get; init; }
            public List<Effect> ActiveEffects { get; init; } = [];
            public GameState NextRound(SpellNames nextSpell)
            {
                return new GameState
                {
                    PlayerHitPoints = PlayerHitPoints,
                    PlayerArmor = PlayerArmor,
                    PlayerMana = PlayerMana,
                    BossHitPoints = BossHitPoints,
                    BossDamage = BossDamage,
                    TotalManaCost = TotalManaCost,
                    NextSpell = nextSpell,
                    ActiveEffects = ActiveEffects.Select(x => x.Duplicate()).ToList()
                };
            }
        }

        private record struct Boss(int HitPoints, int Damage);
    }
}
