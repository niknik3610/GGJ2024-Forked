using System.Collections;
using System.Collections.Generic;
using IngredientDetails;
using UnityEngine;

public class ClientRequestGenerator
{

    public int maxIngredients = 2;
    public Queue<ClientRequest> generateRequests(SpecialClientType clientType)
    {
        return new Queue<ClientRequest>();
    }

    public ClientRequest generateRequest(SpecialClientType clientType)
    {
        var request = new ClientRequest();
        request.Request_required_ingredients = new List<(IngredientDetails.Emotion, Severity)>();

        int cap = Random.Range(1, maxIngredients + 1);
        List<Emotion> usedEmotions = new();
        for(int i = 0; i < cap; i++)
        {
            Emotion emotion = (Emotion)Random.Range(1, 6);
            while(usedEmotions.Contains(emotion))
            {
                emotion = (Emotion)Random.Range(1, 6);
            }
            usedEmotions.Add(emotion);
            Severity severity = Random.Range(0, 1f) < 0.75f ? Severity.Standard : Severity.High;
            request.Request_required_ingredients.Add((emotion, severity));
        }
        Debug.Log("Generated new request:");
        for(int i = 0; i < request.Request_required_ingredients.Count; i++)
        {
            var (em, sev) = request.Request_required_ingredients[i];
            Debug.Log("Emotion: " + em + "; Severity: " + sev);
        }
        return request;
    }
}
