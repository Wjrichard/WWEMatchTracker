import type {Match} from "@/types/match.ts";
import type {Team} from "@/types/team.ts";

export interface MatchDetails
{
    teams: Team[];
    match: Match;
}