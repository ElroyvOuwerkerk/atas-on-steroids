import { useQuery } from "@tanstack/react-query";
import api from "./client";
import type { ReviewResponse } from "../types/tradeV2";

export function useReview() {
  return useQuery<ReviewResponse>({
    queryKey: ["review"],
    queryFn: async () => {
      const { data } = await api.get("/v2/review");
      return data;
    },
  });
}
