import type {Match} from "@/types/match.ts";
import type {Event} from "@/types/event.ts";

export interface EventDetails
{
    matches: Match[];
    event: Event;
}