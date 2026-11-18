// Helpers for the journal v2 form. Kept apart from format.ts so nothing existing
// is touched.

// Dollars per point, suggested per instrument. Micros mirror the minis 1:1 on
// price; only the point value differs. Elroy trades the micros.
const POINT_VALUE: Record<string, number> = {
  MES: 5,
  ES: 50,
  MNQ: 2,
  NQ: 20,
};

export function suggestPointValue(instrument: string): number | null {
  const key = instrument.trim().toUpperCase();
  return POINT_VALUE[key] ?? null;
}

export const KNOWN_INSTRUMENTS = ["MES", "MNQ", "ES", "NQ"];

export const KNOWN_SIGNALS = [
  "",
  "COT_Bull",
  "COT_Bear",
  "COT_1Candle_Bull",
  "COT_1Candle_Bear",
  "MCoC_Bull",
  "MCoC_Bear",
  "COC_Bull",
  "COC_Bear",
  "Moon_Bull",
  "Moon_Bear",
];

// A datetime-local input value ("2026-11-18T14:35") is local wall-clock time.
// Convert it to a real UTC instant so the backend buckets sessions correctly.
export function localInputToISO(value: string): string {
  if (!value) return "";
  const d = new Date(value); // parsed as local time
  if (Number.isNaN(d.getTime())) return "";
  return d.toISOString();
}

// Reverse: an ISO instant to a local datetime-local value for an input.
export function isoToLocalInput(iso: string | null | undefined): string {
  if (!iso) return "";
  const d = new Date(iso);
  if (Number.isNaN(d.getTime())) return "";
  const off = d.getTimezoneOffset() * 60000;
  return new Date(d.getTime() - off).toISOString().slice(0, 16);
}

// Parse a text field to a number, or null when blank.
export function numOrNull(v: string): number | null {
  if (v === "" || v === null || v === undefined) return null;
  const n = Number(v);
  return Number.isFinite(n) ? n : null;
}
