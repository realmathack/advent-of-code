﻿namespace AdventOfCode.Y2015.Solvers
{
    public class Day22(int _playerHitPoints, int _playerMana) : SolverWithLines
    {
        public Day22() : this(50, 500) { }

        private static readonly Dictionary<SpellNames, Spell> _spells = GetSpells();
        private int _lowestManaCost;

        public override object SolvePart1(string[] input) => FindLeastAmountOfMana(ToBoss(input));
        public override object SolvePart2(string[] input) => FindLeastAmountOfMana(ToBoss(input), true);

        private int FindLeastAmountOfMana(Boss boss, bool isHardDifficulty = false)
        {
            _lowestManaCost = int.MaxValue;
            var startingState = new GameState(_playerHitPoints, 0, _playerMana, boss.HitPoints, boss.Damage, 0);
            var possibleMoves = new Queue<GameState>();
            foreach (var spell in _spells)
            {
                possibleMoves.Enqueue(startingState.NextRound(spell.Key));
            }
            while (possibleMoves.TryDequeue(out var current))
            {
                // Player turn
                if (isHardDifficulty)
                {
                    current.PlayerHitPoints--;
                }
                if (IsGameOver(current)) { continue; }
                ExecuteEffects(current);
                if (IsGameOver(current)) { continue; }
                var spellToCast = _spells[current.NextSpell];
                if (current.PlayerMana < spellToCast.Cost || current.ActiveEffects.Any(effect => effect.SpellName == current.NextSpell))
                {
                    continue; // Not enough mana or effect already active
                }
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
                // Boss turn
                if (IsGameOver(current)) { continue; }
                ExecuteEffects(current);
                if (IsGameOver(current)) { continue; }
                current.PlayerHitPoints -= Math.Max(1, current.BossDamage - current.PlayerArmor);
                if (IsGameOver(current)) { continue; }
                // Queue next round
                foreach (var spell in _spells)
                {
                    possibleMoves.Enqueue(current.NextRound(spell.Key));
                }
            }
            return _lowestManaCost;
        }

        private static Boss ToBoss(string[] lines) => new(int.Parse(lines[0].Split(' ')[^1]), int.Parse(lines[1].Split(' ')[^1]));

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

        private static void ExecuteEffects(GameState current)
        {
            for (int i = current.ActiveEffects.Count - 1; i >= 0; i--)
            {
                var effect = current.ActiveEffects[i];
                var spell = _spells[effect.SpellName];
                current.BossHitPoints -= spell.Damage;
                current.PlayerArmor = Math.Max(current.PlayerArmor, spell.Armor);
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

        private static Dictionary<SpellNames, Spell> GetSpells() => new()
        {
            [SpellNames.MagicMissile] = new Spell(SpellNames.MagicMissile, 53, 4, 0, 0, 0, 0),
            [SpellNames.Drain] = new Spell(SpellNames.Drain, 73, 2, 0, 2, 0, 0),
            [SpellNames.Shield] = new Spell(SpellNames.Shield, 113, 0, 7, 0, 0, 6),
            [SpellNames.Poison] = new Spell(SpellNames.Poison, 173, 3, 0, 0, 0, 6),
            [SpellNames.Recharge] = new Spell(SpellNames.Recharge, 229, 0, 0, 0, 101, 5)
        };

        private enum SpellNames { None, MagicMissile, Drain, Shield, Poison, Recharge }
        private record class Spell(SpellNames Name, int Cost, int Damage, int Armor, int Heal, int Mana, int Duration);
        private record class Boss(int HitPoints, int Damage);

        private record class Effect(SpellNames SpellName, int Duration)
        {
            public int Duration { get; set; } = Duration;
            public Effect Duplicate() => new(SpellName, Duration);
        }

        private record class GameState(int PlayerHitPoints, int PlayerArmor, int PlayerMana, int BossHitPoints, int BossDamage, int TotalManaCost)
        {
            public int PlayerHitPoints { get; set; } = PlayerHitPoints;
            public int PlayerArmor { get; set; } = PlayerArmor;
            public int PlayerMana { get; set; } = PlayerMana;
            public int BossHitPoints { get; set; } = BossHitPoints;
            public int BossDamage { get; set; } = BossDamage;
            public int TotalManaCost { get; set; } = TotalManaCost;
            public SpellNames NextSpell { get; init; }
            public List<Effect> ActiveEffects { get; init; } = [];
            public GameState NextRound(SpellNames nextSpell) => this with
            {
                NextSpell = nextSpell,
                ActiveEffects = ActiveEffects.Select(effect => effect.Duplicate()).ToList()
            };
        }
    }
}
