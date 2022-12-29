using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Stove : MonoBehaviour, IInteractable
{
    [SerializeField] TMP_Text woodCountText;
    public byte displayValue => displayVal;
    public GameObject itemUIPrompt => uiPanel;
    public byte itemValue => 0; //not pickable
    public string InteractionPrompt => prompt;
    
    [SerializeField] GameObject[] glowMats;
    [SerializeField] GameObject uiPanel;

    [SerializeField] Image thermometreFill;

    [SerializeField] TMP_Text degreeText;

    [SerializeField] ParticleSystem particleHeaterFire;
    [SerializeField] ParticleSystem particleChimneySmoke;

    [SerializeField] Light heaterLight;

    private string prompt = "Stoke up.";

    public const byte displayVal = 1;
    public const int stoveCapacity = 8;
    public static int stoveLoad = 0;
    int emisionVal = 0;  

    float currentTemp = 20; 
    const int minTemp = -16;
    const float maxTemp = 30;
    const float charTempIncrement = 2f;

    const float stoveCoolRate = 5f; //???
    const float charCoolRate = 500f; //???
    const float therTempIncrement = 0.032f; // temp 20C = 0.837 on the slider
   
    
    public static int woodCount = 0; 
    const int initialWoodCount = 0;

    public static event System.Action<bool> PlayerDiedofCold;

    public void Interact(Interactor interactor)
    {
        CancelInvoke("StoveCoolDown");
        CancelInvoke("CharacterCoolDown");
        
        if (woodCount > 0)
        {
            if (stoveLoad <= stoveCapacity)
            {
                if (woodCount > 0)
                {
                    woodCount--;
                    woodCountText.text = "x" + woodCount;                    
                }
                stoveLoad++;
            
                if (stoveLoad > 0)
                {  
                    ChatacterSetTemperature(true);
                }

                var particle = particleHeaterFire.main;

                if (particle.startSize.constant < 6f)
                {
                    emisionVal += 1;
                    particle.startSize = emisionVal;   
                    GetComponent<AudioSource>().volume += 0.2f;
                }

                if (heaterLight.intensity < 1)
                {
                    heaterLight.intensity += 0.1f;                 
                }

                particleHeaterFire.Play();
                particleChimneySmoke.Play();
            }

            if (stoveLoad >= stoveCapacity)
            {
                uiPanel.SetActive(false);
                InteractionPromptUI.canActivate = false;
            }
           
        }
   
        InvokeRepeating("StoveCoolDown", 10, stoveCoolRate);
    }

    public void EndInteraction(Interactor interactor)
    {
        foreach (GameObject mat in glowMats)
        {
            mat.GetComponent<Renderer>().material.SetColor("_GlowColor", (new Color(191, 81, 0, 0) * 0.02f));
        }
        uiPanel.SetActive(false);
    }

    void StoveCoolDown()
    {
        var particle = particleHeaterFire.main;

        if (stoveLoad > 0)
        {            
            stoveLoad--;
            InteractionPromptUI.canActivate = true;
        }

        if (particle.startSize.constant > 0)
        {
            emisionVal--;
            particle.startSize = emisionVal;        
        }

        if (heaterLight.intensity > 0)
        {
            GetComponent<AudioSource>().volume -= 0.15f;
            heaterLight.intensity -= 0.008f;
        }

        if (emisionVal <= 0)
        {
            particleHeaterFire.Stop();
            particleChimneySmoke.Stop();
            heaterLight.intensity = 0;
            CancelInvoke("StoveCoolDown");
            ChatacterSetTemperature(false);
        }
    }

    void ChatacterSetTemperature(bool isHeatingUp)
    {
        if (isHeatingUp && stoveLoad > 0)
        {
            CancelInvoke("CharacterCoolDown");
            CancelInvoke("CharacterHeatUp");
            InvokeRepeating("CharacterHeatUp", 1f, 4f);
        }

        if (!isHeatingUp)
        {
            CancelInvoke("CharacterHeatUp");
            InvokeRepeating("CharacterCoolDown", 1f, charCoolRate * Time.deltaTime);
        }
    }

    void CharacterCoolDown()
    {
        if (currentTemp >= minTemp && thermometreFill.fillAmount >= (0.254f + therTempIncrement))
        {
            thermometreFill.fillAmount -= therTempIncrement;
            currentTemp -= charTempIncrement;

            degreeText.text = currentTemp.ToString();
        }

        if(currentTemp <= minTemp)
        {
            CancelInvoke("CharacterCoolDown");
            PlayerDiedofCold?.Invoke(true);
        }
    }

    void CharacterHeatUp()
    {
        if(currentTemp < maxTemp && thermometreFill.fillAmount <= (1 - therTempIncrement))
        {           
            thermometreFill.fillAmount += therTempIncrement;
            currentTemp += charTempIncrement;

            degreeText.text = currentTemp.ToString();
        }

        if(currentTemp >= maxTemp)
        {
            CancelInvoke("CharacterHeatedUp");
        }
    }


    #region Initials / Utils
    private void Awake()
    {
        prompt = "Stoke up.";
        stoveLoad = 0;
        
        woodCount = initialWoodCount;
        woodCountText.text = "x" + woodCount;
       
        CancelInvoke("CharacterCoolDown");
       
        degreeText.text = currentTemp.ToString();
    }

    private void Start()
    {
        InvokeRepeating("CharacterCoolDown", 1f, 10);
    }

    void UpdateWoodCountInventory()
    {
        woodCount++;
        woodCountText.text = "x" + woodCount;
    }

    private void OnEnable()
    {
        Character.inStokingRange += TurnoffGlow;
        Character.inStoveRange += ChatacterSetTemperature;
        Character.woodPicked += UpdateWoodCountInventory;

    }
    private void OnDisable()
    {
        Character.inStokingRange -= TurnoffGlow;
        Character.inStoveRange -= ChatacterSetTemperature;
        Character.woodPicked += UpdateWoodCountInventory;
    }

    void TurnoffGlow()
    {
        foreach (GameObject mat in glowMats)
        {
            mat.GetComponent<Renderer>().material.SetColor("_GlowColor", new Color(0, 0, 0, 0));
        }
    }
    #endregion
}
