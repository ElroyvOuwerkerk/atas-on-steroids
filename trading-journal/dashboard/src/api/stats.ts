import { useQuery } from "@tanstack/react-query";
import api from "./client";
import type { StatsResponse } from "../types/trade";

export function useStats() {
  return useQuery<StatsResponse>({
    queryKey: ["stats"],
    queryFn: async () => {
      const { data } = await api.get("/stats");
      return data;
    },
  });
}
