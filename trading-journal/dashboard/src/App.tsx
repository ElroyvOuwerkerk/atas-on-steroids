import { Routes, Route, NavLink, Navigate } from "react-router-dom";
import Trades from "./pages/Trades";
import TradeView from "./pages/TradeView";
import Stats from "./pages/Stats";

function NavTab({ to, children }: { to: string; children: React.ReactNode }) {
  return (
    <NavLink
      to={to}
      className={({ isActive }) =>
        `px-4 py-2 text-sm font-medium rounded-lg transition-colors ${
          isActive
            ? "bg-zinc-700 text-zinc-100"
            : "text-zinc-400 hover:text-zinc-200 hover:bg-zinc-800"
        }`
      }
    >
      {children}
    </NavLink>
  );
}

export default function App() {
  return (
    <div className="min-h-screen">
      {/* Nav bar */}
      <nav className="border-b border-zinc-800 px-6 py-3 flex items-center gap-2">
        <span className="text-sm font-semibold text-zinc-200 mr-4">
          Trading Journal
        </span>
        <NavTab to="/trades">Trades</NavTab>
        <NavTab to="/stats">Stats</NavTab>
      </nav>

      {/* Page content */}
      <main className="max-w-5xl mx-auto px-6 py-6">
        <Routes>
          <Route path="/" element={<Navigate to="/trades" replace />} />
          <Route path="/trades" element={<Trades />} />
          <Route path="/trades/:id" element={<TradeView />} />
          <Route path="/stats" element={<Stats />} />
        </Routes>
      </main>
    </div>
  );
}
