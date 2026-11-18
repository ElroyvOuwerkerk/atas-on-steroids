import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import api from "./client";
import type { TradeV2, TradeV2Create, TradeV2Update, ExitIn } from "../types/tradeV2";

interface V2Filters {
  strategy?: string;
  instrument?: string;
  direction?: string;
  from?: string;
  to?: string;
}

export function useV2Trades(filters: V2Filters = {}) {
  return useQuery<TradeV2[]>({
    queryKey: ["v2trades", filters],
    queryFn: async () => {
      const params: Record<string, string> = {};
      for (const [k, v] of Object.entries(filters)) {
        if (v) params[k] = v;
      }
      const { data } = await api.get("/v2/trades", { params });
      return data;
    },
  });
}

export function useV2Trade(id: number) {
  return useQuery<TradeV2>({
    queryKey: ["v2trade", id],
    enabled: Number.isFinite(id) && id > 0,
    queryFn: async () => {
      const { data } = await api.get(`/v2/trades/${id}`);
      return data;
    },
  });
}

export function useCreateV2Trade() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: async (body: TradeV2Create) => {
      const { data } = await api.post("/v2/trades", body);
      return data as TradeV2;
    },
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["v2trades"] });
      qc.invalidateQueries({ queryKey: ["review"] });
    },
  });
}

export function useUpdateV2Trade(id: number) {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: async (body: TradeV2Update) => {
      const { data } = await api.put(`/v2/trades/${id}`, body);
      return data as TradeV2;
    },
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["v2trade", id] });
      qc.invalidateQueries({ queryKey: ["v2trades"] });
      qc.invalidateQueries({ queryKey: ["review"] });
    },
  });
}

export function useAddExit(id: number) {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: async (body: ExitIn) => {
      const { data } = await api.post(`/v2/trades/${id}/exits`, body);
      return data as TradeV2;
    },
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["v2trade", id] });
      qc.invalidateQueries({ queryKey: ["v2trades"] });
      qc.invalidateQueries({ queryKey: ["review"] });
    },
  });
}

export function useDeleteV2Trade() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: async (id: number) => {
      await api.delete(`/v2/trades/${id}`);
      return id;
    },
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["v2trades"] });
      qc.invalidateQueries({ queryKey: ["review"] });
    },
  });
}
