using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;



namespace  Model
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class FirewallRules : IEquatable<FirewallRules>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirewallRules" /> class.
        /// </summary>
        public FirewallRules()
        {

        }


        /// <summary>
        /// The resource's unique identifier
        /// </summary>
        /// <value>The resource's unique identifier</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }


        /// <summary>
        /// The type of object that has been created
        /// </summary>
        /// <value>The type of object that has been created</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }


        /// <summary>
        /// URL to the object’s representation (absolute path)
        /// </summary>
        /// <value>URL to the object’s representation (absolute path)</value>
        [DataMember(Name = "href", EmitDefaultValue = false)]
        public string Href { get; set; }


        /// <summary>
        /// Array of items in that collection
        /// </summary>
        /// <value>Array of items in that collection</value>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public List<FirewallRule> Items { get; set; }



        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FirewallRules {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Href: ").Append(Href).Append("\n");
            sb.Append("  Items: ").Append(Items).Append("\n");

            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as FirewallRules);
        }

        /// <summary>
        /// Returns true if FirewallRules instances are equal
        /// </summary>
        /// <param name="other">Instance of FirewallRules to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FirewallRules other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return
                (
                    this.Id == other.Id ||
                    this.Id != null &&
                    this.Id.Equals(other.Id)
                ) &&
                (
                    this.Type == other.Type ||
                    this.Type != null &&
                    this.Type.Equals(other.Type)
                ) &&
                (
                    this.Href == other.Href ||
                    this.Href != null &&
                    this.Href.Equals(other.Href)
                ) &&
                (
                    this.Items == other.Items ||
                    this.Items != null &&
                    this.Items.SequenceEqual(other.Items)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)

                if (this.Id != null)
                    hash = hash * 59 + this.Id.GetHashCode();

                if (this.Type != null)
                    hash = hash * 59 + this.Type.GetHashCode();

                if (this.Href != null)
                    hash = hash * 59 + this.Href.GetHashCode();

                if (this.Items != null)
                    hash = hash * 59 + this.Items.GetHashCode();

                return hash;
            }
        }

    }


}
