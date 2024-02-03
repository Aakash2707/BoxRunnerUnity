using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using VoxelBusters.CoreLibrary;
using VoxelBusters.EssentialKit;
using UnityEngine.SceneManagement;
using TMPro;
public class AddressBookUI : MonoBehaviour
{
    // Reference to the ScrollRect in the UI Canvas
    public ScrollRect addressBookScrollRect;
    // Reference to the prefab for a single contact entry
    public GameObject contactEntryPrefab;
    public  GameObject backButton;
    public List<IAddressBookContact> contacts;
    public void PopulateAddressBook()
    {
        
         
        // Loop through each contact and create an entry in the UI
        foreach (IAddressBookContact contact in contacts)
        {
            Debug.Log("adsadhaso"+ contact.ToString());
            // Instantiate a contact entry prefab
            GameObject contactEntry = Instantiate(contactEntryPrefab, addressBookScrollRect.content);
            // Set the contact's first name and last name in the entry UI
            TextMeshProUGUI firstNameText = contactEntry.transform.Find("FirstName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI lastNameText = contactEntry.transform.Find("LastName").GetComponent<TextMeshProUGUI>();
            // Display the contact's first name and last name in the UI
            firstNameText.text = contact.FirstName.ToString();
            lastNameText.text =contact.LastName.ToString();
            
            if(contact.EmailAddresses.Length == 0 ){
                contactEntry.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    } 
}
