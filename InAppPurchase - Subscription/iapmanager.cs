using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class iapmanager : MonoBehaviour, IStoreListener
{
    public static iapmanager instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    User _user;
    public GameObject restoreBtn;

    private string monthlySubscription_id = "com.appm.triggerpoint.monthly";
    private string yearlySubscription_id = "com.appm.tp.yearlyplan";

    public GameObject NeedlineImages, SafetyImages, Buybobscreenbutton, allregionsBtn, bobbuyscreen, premiumScree, note, oneTimeBuyButton;

    public Text textOutput;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        if (SceneManager.GetActiveScene().buildIndex == 0 && Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if(restoreBtn != null)
            {
                restoreBtn.SetActive(true);
            }
            
        }
    }


    private void Start()
    {
        _user = GlobalScript.instance.user;
        
    }


    public void InitializePurchasing()
    {
        textOutput.text = "1";

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(monthlySubscription_id, ProductType.Subscription);
        builder.AddProduct(yearlySubscription_id, ProductType.Subscription);

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        textOutput.text = "2";
        Debug.Log("In-App Purchasing successfully initialized");
        m_StoreController = controller;

        UpdateUI();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);

        Loader.instance.transform.GetChild(0).gameObject.SetActive(false);
        textOutput.text = "3";
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        Loader.instance.transform.GetChild(0).gameObject.SetActive(true);
        textOutput.text = "4";

        var product = purchaseEvent.purchasedProduct;

        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        UpdateUI();

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"{product} purchase failed due to: {failureReason}");

        Loader.instance.transform.GetChild(0).gameObject.SetActive(false);
        textOutput.text = "5";
    }


    //Holds subscription information - isCancelled, isNull, isValid, isExpired, isSubscribed
    public bool IsSubscribedTo(Product subscription)
    {
        if (subscription.receipt == null)
        {
            return false;
        }

        var subscriptionManager = new SubscriptionManager(subscription, null);

        var info = subscriptionManager.getSubscriptionInfo();

        if (info.isFreeTrial() == Result.True)
        {
            GlobalScript.instance.onFreeTrialPeriod = true;
            GlobalScript.instance.isSubscribed = true;
            PlayerPrefs.SetInt("IsSubscribed", 1);
            return true;
        }
        else if (info.isSubscribed() == Result.True)
        {
            GlobalScript.instance.isSubscribed = true;
            PlayerPrefs.SetInt("IsSubscribed", 1);
            GlobalScript.instance.onFreeTrialPeriod = false;
            return true;
        }
        else if (info.isSubscribed() == Result.False)
        {
            GlobalScript.instance.isSubscribed = false;
            PlayerPrefs.SetInt("IsSubscribed", 0);
            GlobalScript.instance.onFreeTrialPeriod = false;
            return false;
        }else if(info.isExpired() == Result.True)
        {
            GlobalScript.instance.isSubscribed = false;
            PlayerPrefs.SetInt("IsSubscribed", 0);
            GlobalScript.instance.onFreeTrialPeriod = false;
            return false;
        }else if(info.isCancelled() == Result.True)
        {
            GlobalScript.instance.isSubscribed = false;
            PlayerPrefs.SetInt("IsSubscribed", 0);
            GlobalScript.instance.onFreeTrialPeriod = false;
            return false;
        }

        GlobalScript.instance.isSubscribed = true;
        PlayerPrefs.SetInt("IsSubscribed", 1);
        return true;
    }


    public void UpdateUI()
    {
        Loader.instance.transform.GetChild(0).gameObject.SetActive(true);
        Loader.instance.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Loading";

        textOutput.text = "6";

        var monthly_subscriptionProduct = m_StoreController.products.WithID(monthlySubscription_id);
        var yearly_subscriptionProduct = m_StoreController.products.WithID(yearlySubscription_id);

        try
        {
            var isSubscribed_monthly = IsSubscribedTo(monthly_subscriptionProduct);
            var isSubscribed_yearly = IsSubscribedTo(yearly_subscriptionProduct);

            if (PlayerPrefs.GetInt("LoggedIn") == 1)
            {
                if (isSubscribed_monthly || isSubscribed_yearly)
                {
                    PlayerPrefs.SetInt("tutorial", 1);
                    PlayerPrefs.SetInt("buyapp", 1);
                    PlayerPrefs.SetInt("FaceHead", 1);
                    PlayerPrefs.SetInt("AllRegions", 1);
                    PlayerPrefs.SetInt("LegsAndFoot", 1);
                    PlayerPrefs.SetInt("HipsAndThigh", 1);
                    PlayerPrefs.SetInt("LumboPelvis", 1);
                    PlayerPrefs.SetInt("SpineTorsoo", 1);
                    PlayerPrefs.SetInt("ForearmHand", 1);
                    PlayerPrefs.SetInt("ShoulderUpperArm", 1);
                    PlayerPrefs.SetInt("popup", 1);

                    
                    if (SceneManager.GetActiveScene().name == "0Main3DSearchIOSNewUIAndroid")
                    {
                        Debug.Log("you have Successfully Subscribed --- TriggerPoint app unlocked !!!!");

                        Loader.instance.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Loading Face & Head scene";
                        //Loader.instance.transform.GetChild(0).gameObject.SetActive(false);

                        SceneManager.LoadScene("1portrait_main");
                    }
                    
                }
                else
                {
                    PlayerPrefs.SetInt("tutorial", 0);
                    PlayerPrefs.SetInt("buyapp", 0);
                    PlayerPrefs.SetInt("freetrial", 0);
                    PlayerPrefs.SetInt("FaceHead", 0);
                    PlayerPrefs.SetInt("AllRegions", 0);
                    PlayerPrefs.SetInt("LegsAndFoot", 0);
                    PlayerPrefs.SetInt("HipsAndThigh", 0);
                    PlayerPrefs.SetInt("LumboPelvis", 0);
                    PlayerPrefs.SetInt("SpineTorsoo", 0);
                    PlayerPrefs.SetInt("ForearmHand", 0);
                    PlayerPrefs.SetInt("ShoulderUpperArm", 0);
                    PlayerPrefs.SetInt("popup", 0);

                    Debug.Log("you have Not Subscribed or Recipt isNull --- TriggerPoint app locked !!!!");
                    if (SceneManager.GetActiveScene().name == "0Main3DSearchIOSNewUIAndroid")
                    {
                        if (PlayerPrefs.GetInt("LoggedIn") == 1)
                        {
                            UIManager.main.freetrialScreen.SetActive(true);
                            UIManager.main.loginPanel.SetActive(false);
                            UIManager.main.registrationPanel.SetActive(false);
                        }
                        else
                        {
                            UIManager.main.loginPanel.SetActive(true);
                            UIManager.main.freetrialScreen.SetActive(false);
                            UIManager.main.registrationPanel.SetActive(false);
                        }
                        Loader.instance.transform.GetChild(0).gameObject.SetActive(false);
                    }

                    if (SceneManager.GetActiveScene().name != "0Main3DSearchIOSNewUIAndroid")
                    {
                        if (PlayerPrefs.GetInt("LoggedIn") == 1)
                        {
                            UIManager.main.freetrialScreen.SetActive(true);
                            UIManager.main.loginPanel.SetActive(false);
                            UIManager.main.registrationPanel.SetActive(false);
                        }
                        else
                        {
                            UIManager.main.loginPanel.SetActive(true);
                            UIManager.main.freetrialScreen.SetActive(false);
                            UIManager.main.registrationPanel.SetActive(false);
                        }
                        Loader.instance.transform.GetChild(0).gameObject.SetActive(false);

                        SceneManager.LoadScene("0Main3DSearchIOSNewUIAndroid");
                    }

                }
                //Loader.instance.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        catch (StoreSubscriptionInfoNotSupportedException)
        {
            Debug.Log("Catch Exception!!!  StoreSubscriptionInfoNotSupportedException");

            Loader.instance.transform.GetChild(0).gameObject.SetActive(false);
            textOutput.text = "15";
        }

    }

    public void OnPurchaseComplete(Product product)
    {
        GlobalScript.instance.isSubscribed = true;
        PlayerPrefs.SetInt("IsSubscribed", 1);

        PlayerPrefs.SetInt("tutorial", 1);
        PlayerPrefs.SetInt("buyapp", 1);
        PlayerPrefs.SetInt("FaceHead", 1);
        PlayerPrefs.SetInt("AllRegions", 1);
        PlayerPrefs.SetInt("LegsAndFoot", 1);
        PlayerPrefs.SetInt("HipsAndThigh", 1);
        PlayerPrefs.SetInt("LumboPelvis", 1);
        PlayerPrefs.SetInt("SpineTorsoo", 1);
        PlayerPrefs.SetInt("ForearmHand", 1);
        PlayerPrefs.SetInt("ShoulderUpperArm", 1);
        PlayerPrefs.SetInt("popup", 1);


        Loader.instance.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Loading Face & Head scene";
        SceneManager.LoadScene("1portrait_main");
    }


    public void PassSubscriptionScreen()
    {
        Debug.Log("you have Successfully Subscribed --- TriggerPoint app unlocked !!!!");
        GlobalScript.instance.isSubscribed = true;
        PlayerPrefs.SetInt("IsSubscribed", 1);

        GlobalScript.instance.isLogin = true;
        PlayerPrefs.SetInt("tutorial", 1);
        PlayerPrefs.SetInt("buyapp", 1);
        PlayerPrefs.SetInt("FaceHead", 1);
        PlayerPrefs.SetInt("AllRegions", 1);
        PlayerPrefs.SetInt("LegsAndFoot", 1);
        PlayerPrefs.SetInt("HipsAndThigh", 1);
        PlayerPrefs.SetInt("LumboPelvis", 1);
        PlayerPrefs.SetInt("SpineTorsoo", 1);
        PlayerPrefs.SetInt("ForearmHand", 1);
        PlayerPrefs.SetInt("ShoulderUpperArm", 1);
        PlayerPrefs.SetInt("popup", 1);


        if (SceneManager.GetActiveScene().name != "1portrait_main" && PlayerPrefs.GetInt("LoggedIn") == 1)
        {
            Debug.Log("you have Successfully Subscribed --- TriggerPoint app unlocked !!!!");

            SceneManager.LoadScene("1portrait_main");
        }
    }


    public void RestorePurchases()
    {
        Loader.instance.transform.GetChild(0).gameObject.SetActive(true);
        Loader.instance.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Loading Purchases!";

        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");

            Loader.instance.transform.GetChild(0).gameObject.SetActive(false);
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) =>
            {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
            Loader.instance.transform.GetChild(0).gameObject.SetActive(false);
        }
        // Otherwise ...
        else
        {
            Loader.instance.transform.GetChild(0).gameObject.SetActive(false);

            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);


        }

    }

    public void BuyProductSubscription(string productId)
    {
        Loader.instance.transform.GetChild(0).gameObject.SetActive(true);
        textOutput.text = "13";
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Loader.instance.transform.GetChild(0).gameObject.SetActive(false);
                textOutput.text = "14";
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Loader.instance.transform.GetChild(0).gameObject.SetActive(false);
            textOutput.text = "15";
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

        private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void TurnLoaderOn()
    {
        Loader.instance.transform.GetChild(0).gameObject.SetActive(true);
        Loader.instance.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Loading";

        textOutput.text = "7";
    }

    public void TurnLoaderOff()
    {
        Loader.instance.transform.GetChild(0).gameObject.SetActive(false);
        
        textOutput.text = "7";
    }

}



