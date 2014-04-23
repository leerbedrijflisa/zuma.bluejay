using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Lisa.Zuma.BlueJay.Web.Attributes
{
    public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        public RequiredIfAttribute(string dependentProperty, params object[] targetValue)
        {
            this.dependentProperty = dependentProperty;
            this.targetValue = targetValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get a reference to the property this validation depends upon
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(this.dependentProperty);

            if (field != null)
            {
                // Get the value of the dependent property
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);

                foreach (var obj in targetValue)
                {
                    // Compare value against the target value
                    if ((dependentValue == null && this.targetValue == null) ||
                        (dependentValue != null && dependentValue.Equals(obj)))
                    {
                        // Match => we should try validating this field
                        if (!innerAttribute.IsValid(true))
                        {
                            // Validation failed, return error
                            return new ValidationResult(this.ErrorMessage, new[] 
                            {
                                validationContext.MemberName
                            });
                        }
                    }
                }
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "requiredif"
            };

            var depProp = BuildDependentPropertyId(metadata, context as ViewContext);

            // Find the value on the control we depend on;
            // If it's a bool, format it javascript style
            // (default is True or False!)
            var sb = new StringBuilder();
            foreach (var obj in this.targetValue)
            {
                var targetValue = (obj ?? "").ToString();

                if (obj.GetType() == typeof(bool))
                {
                    targetValue = targetValue.ToLower();
                }

                sb.AppendFormat("|{0}", targetValue);
            }

            rule.ValidationParameters.Add("dependentproperty", depProp);
            rule.ValidationParameters.Add("targetvalue", sb.ToString().TrimStart('|'));

            yield return rule;
        }

        private string BuildDependentPropertyId(ModelMetadata metadata, ViewContext viewContext)
        {
            // Build id of the property
            var depProp = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(this.dependentProperty);

            // unfortunately this will have the name of the current field appended to the beginning,
            // because the TemplateInfo's context has had this fieldname appended to it. Instead, we
            // want to get the context as though it was one level higher (i.e. outside the current property,
            // which is the containing object (our Person), and hence the same level as the dependent property.
            var thisField = metadata.PropertyName + "_";
            if (depProp.StartsWith(thisField))
            {
                // Strip it off
                depProp = depProp.Substring(thisField.Length);
            }

            return depProp;
        }

        private RequiredAttribute innerAttribute = new RequiredAttribute();
        private string dependentProperty;
        private object[] targetValue;
    }
}