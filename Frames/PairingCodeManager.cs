using FrameIt.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace FrameIt.Services
{
    public enum PairingCodeValidationResult
    {
        Valid,
        NotFound,
        AlreadyUsed
    }

    public class PairingCodeManager
    {
        private const string PairingCodesFilePath = "Data/FramePairingCodes.json";
        private List<PairingCodeItem> _pairingCodes = new();

        public PairingCodeManager()
        {
            LoadPairingCodes();
        }

        // ======================
        // LOAD / SAVE
        // ======================

        private void LoadPairingCodes()
        {
            if (!File.Exists(PairingCodesFilePath))
            {
                _pairingCodes = new List<PairingCodeItem>();
                return;
            }

            var json = File.ReadAllText(PairingCodesFilePath);

            _pairingCodes = JsonSerializer.Deserialize<List<PairingCodeItem>>(json)
                            ?? new List<PairingCodeItem>();
        }

        private void SavePairingCodes()
        {
            var json = JsonSerializer.Serialize(
                _pairingCodes,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(PairingCodesFilePath, json);
        }

        // ======================
        // VALIDATION
        // ======================

        public PairingCodeValidationResult Validate(string code)
        {
            code = code?.Trim();

            var pairing = _pairingCodes.FirstOrDefault(p =>
                p.Code?.Trim().Equals(code, StringComparison.OrdinalIgnoreCase) == true);

            if (pairing == null)
                return PairingCodeValidationResult.NotFound;

            if (pairing.Used)
                return PairingCodeValidationResult.AlreadyUsed;

            return PairingCodeValidationResult.Valid;
        }

        // ======================
        // STATE CHANGE
        // ======================

        public bool MarkAsUsed(string code)
        {
            var pairing = _pairingCodes.FirstOrDefault(p =>
                p.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (pairing == null || pairing.Used)
                return false;

            pairing.Used = true;
            SavePairingCodes();
            return true;
        }

        // ======================
        // OPTIONAL HELPERS
        // ======================

        public bool Exists(string code)
        {
            return _pairingCodes.Any(p =>
                p.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<PairingCodeItem> GetAll()
        {
            return _pairingCodes.AsReadOnly();
        }
    }
}
