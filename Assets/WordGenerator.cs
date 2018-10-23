using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour {

	private static string[] easyList = { "herd", "nail", "kill", "flat", "tick", "deck", "feel", "kick", "pole",
                                        "camp", "gear", "band", "duty", "bend", "deep", "coin", "mass", "disk",
                                        "myth", "land", "hill", "snub", "sofa", "race", "play", "plug", "suit",
                                        "shot", "bill", "brag", "halt", "area", "lane", "sick", "hair", "gown",
                                        "mine", "raid", "open", "mist", "tell", "seem", "tear", "lack", "tidy",
                                        "fist", "post", "hero", "dare", "swim", "keep", "taxi", "view", "stem",
                                        "pier", "golf", "plan", "rage", "love", "lump", "maid", "prey", "calm",
                                        "damn", "belt", "solo", "sail", "road", "time", "back", "hell", "wing",
                                        "dome", "fish", "like", "year", "seat", "wave", "fire", "folk" };

    private static string[] mediumList = { "screen", "orange", "salmon", "second", "absorb", "church", "school", "ballet", "review",
                                            "safety", "sleeve", "script", "accept", "energy", "attack", "escape", "detail", "player",
                                            "regret", "patrol", "packet", "bucket", "effort", "offset", "suffer", "normal", "temple",
                                            "stride", "valley", "defend", "endure", "prison", "patent", "sister", "killer", "notion",
                                            "window", "choice", "profit", "retain", "forest", "cherry", "corpse", "figure", "happen",
                                            "shadow", "remedy", "medium", "source", "artist", "exotic", "labour", "flower", "output",
                                            "carpet", "refund", "morale", "option", "shorts", "garlic", "mutual", "runner", "access",
                                            "summer", "arrest", "tiptoe", "silver", "foster", "jungle", "system", "season", "prefer",
                                            "couple", "deport", "embark", "sketch", "twitch", "nuance", "letter", "monkey" };

    private static string[] hardList = { "critical", "restless", "minister", "terminal", "conceive", "football", "talented", "unlikely", "employee", "compound", "contract",
                                        "unlawful", "increase", "question", "autonomy", "flexible", "domestic", "skeleton", "industry", "surround", "hardware", "creation",
                                        "bathroom", "tropical", "epicalyx", "variable", "governor", "behavior", "audience", "mushroom", "rational", "addicted", "dividend",
                                        "grateful", "collapse", "attitude", "patience", "customer", "straight", "graduate", "diameter", "indirect", "syndrome", "traction",
                                        "vigorous", "exposure", "carriage", "standard", "joystick", "definite", "explicit", "adoption", "emphasis", "business", "feminine",
                                        "affinity", "imposter", "password", "mourning", "disagree", "arrogant", "familiar", "casualty", "vertical", "greeting", "medicine",
                                        "conflict", "railroad", "motorist", "shoulder", "mosquito", "innocent", "literacy", "engineer", "crackpot", "disaster", "cultural",
                                        "negative", "portrait", "perceive" };

    private static string[] finalList = { "retirement", "motivation", "temptation", "brainstorm", "restaurant", "revolution", "brilliance", "reasonable", "admiration", "wilderness",
                                        "appearance", "decoration", "preference", "thoughtful", "generation", "democratic", "federation", "prevalence", "diplomatic",
                                        "chauvinist", "basketball", "fastidious", "particular", "projection", "regulation", "excavation", "assessment", "provincial",
                                        "enthusiasm", "negligence", "stereotype", "attachment", "separation", "hypnothize", "disability", "assignment", "continuous",
                                        "censorship", "commission", "substitute", "first-hand", "convention", "girlfriend", "systematic", "obligation", "television",
                                        "discourage", "investment", "competence", "transition", "curriculum", "concession", "litigation", "artificial", "incredible",
                                        "withdrawal", "relinquish", "management", "inhibition", "technology", "opposition", "conference", "attractive", "atmosphere",
                                        "right wing", "acceptable", "articulate", "confession", "excitement", "houseplant", "mechanical", "mainstream", "appreciate",
                                        "allocation", "unpleasant", "functional", "proportion", "convulsion", "compliance" };

    public static string GetRandomWord ()
	{
        string[] ar = getStringArray();
        int randomIndex = Random.Range(0, ar.Length);
        string randomWord = ar[randomIndex];
        if (Data.isNetwork)
            Data.EnemyWords = randomWord;
        return randomWord;

    }

    public static void setCategory(string _category)
    {
        Data.LevelCategory = _category;
    }

    private static string[] getStringArray()
    {
        if (Data.LevelCategory == "Easy")
        {
            return easyList;
        } 
        else if (Data.LevelCategory == "Medium")
        {
            return mediumList;
        }
        else if (Data.LevelCategory == "Hard")
        {
            return hardList;
        }
        else if (Data.LevelCategory == "Final")
        {
            return finalList;
        }
        else
        {
            return easyList;
        }
    }

}
