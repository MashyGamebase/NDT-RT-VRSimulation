using HPhysic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public enum ConnectionType
{
    Power = 0,
    Camera = 1
}

public class ConnectorProxy : MonoBehaviour
{
    [SerializeField] private ConnectionType connectionType;
    [SerializeField] private NDTSourceGetImage GetSourceImage => NDTSourceGetImage.Instance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<XRGrabInteractable>())
        {
            if (!other.gameObject.GetComponent<XRGrabInteractable>().isSelected)
            {
                if (other.gameObject.GetComponent<Connector>())
                {
                    Connector otherConnector = other.gameObject.GetComponent<Connector>();
                    GetComponent<Connector>().Connect(otherConnector);
                    otherConnector.Connect(this.GetComponent<Connector>());

                    switch (connectionType)
                    {
                        case ConnectionType.Power:
                            GetSourceImage.TogglePower(true);
                            break;
                        case ConnectionType.Camera:
                            GetSourceImage.ToggleCamera(true);
                            break;
                    }
                }
            }
            else
            {
                if (other.gameObject.GetComponent<Connector>())
                {
                    Connector otherConnector = other.gameObject.GetComponent<Connector>();
                    GetComponent<Connector>().Disconnect(otherConnector);
                    otherConnector.Disconnect(this.GetComponent<Connector>());

                    switch (connectionType)
                    {
                        case ConnectionType.Power:
                            GetSourceImage.TogglePower(false);
                            break;
                        case ConnectionType.Camera:
                            GetSourceImage.ToggleCamera(false);
                            break;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<XRGrabInteractable>())
        {
            if (other.gameObject.GetComponent<XRGrabInteractable>().isSelected)
            {
                if (other.gameObject.GetComponent<Connector>())
                {
                    Connector otherConnector = other.gameObject.GetComponent<Connector>();
                    GetComponent<Connector>().Disconnect(otherConnector);
                    otherConnector.Disconnect(this.GetComponent<Connector>());
                }
            }
        }
    }
}