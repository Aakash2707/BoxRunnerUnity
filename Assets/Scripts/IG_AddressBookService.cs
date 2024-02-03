using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using VoxelBusters.CoreLibrary;
using VoxelBusters.EssentialKit;
public class IG_AddressBookService : MonoBehaviour
{
    // Variables to store contacts access status and all contacts
    AddressBookContactsAccessStatus status;
    public IAddressBookContact[] allContacts;
    // Reference to the AddressBookUI for updating contacts UI
    public AddressBookUI addressUi;
    // Start is called before the first frame update
    void Start()
    {
        ReadContacts();// Initialize by reading contacts
    }
     // Method to initiate reading of contacts
    public void ReadContacts(){
        status = AddressBook.GetContactsAccessStatus();
        if(status == AddressBookContactsAccessStatus.NotDetermined)
        {
            AddressBook.RequestContactsAccess(callback : OnRequestContactsAccessFinish);
        }
        if(status == AddressBookContactsAccessStatus.Authorized)
        {
            AddressBook.ReadContacts(OnReadContactsFinish);
        }
    }
    // Callback for handling the result of the contacts access request
    private void OnRequestContactsAccessFinish(AddressBookRequestContactsAccessResult result, Error error)
    {
        Debug.Log("Request for contacts access finished.");
        Debug.Log("Address book contacts access status: " + result.AccessStatus);
        if(result.AccessStatus == AddressBookContactsAccessStatus.Authorized)
        {
            AddressBook.ReadContacts(OnReadContactsFinish);
        }
    }  
    // Callback for handling the result of reading contacts
    private void OnReadContactsFinish(AddressBookReadContactsResult result, Error error)
    {
        if (error == null)
        {
            allContacts = result.Contacts;
            var contacts = result.Contacts;
            Debug.Log("Request to read contacts finished successfully.");
            Debug.Log("Total contacts fetched: " + contacts.Length);
            Debug.Log("Below are the contact details (capped to first 10 results only):");
            for (int iter = 0; iter < contacts.Length && iter < 10; iter++)
            {
                Debug.Log(string.Format("[{0}]: {1}", iter, contacts[iter]));
            }
            Debug.Log(allContacts);
        }
        else
        {
            Debug.Log("Request to read contacts failed with error. Error: " + error);
        }
        // Update UI with the list of contacts and populate the address book UI
        addressUi.contacts = allContacts.ToList();
        addressUi.PopulateAddressBook();
    } 
    
}
