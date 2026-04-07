import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import api from "./client";
import type { Trade } from "../types/trade";

interface TradeFilters {
  signal?: string;
  from?: string;
  to?: string;
}

export function useTrades(filters: TradeFilters = {}) {
  return useQuery<Trade[]>({
    queryKey: ["trades", filters],
    queryFn: async () => {
      const params: Record<string, string> = {};
      if (filters.signal) params.signal = filters.signal;
      if (filters.from) params.from = filters.from;
      if (filters.to) params.to = filters.to;
      const { data } = await api.get("/trades", { params });
      return data;
    },
  });
}

export function useTrade(id: number) {
  return useQuery<Trade>({
    queryKey: ["trade", id],
    queryFn: async () => {
      const { data } = await api.get(`/trades/${id}`);
      return data;
    },
  });
}

export function useUpdateNotes(id: number) {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: async (notes: string) => {
      const { data } = await api.put(`/trades/${id}/notes`, { notes });
      return data as Trade;
    },
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["trade", id] });
      qc.invalidateQueries({ queryKey: ["trades"] });
    },
  });
}
