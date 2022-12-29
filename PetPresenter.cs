using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSHARPCRUDMVC.Models;
using CSHARPCRUDMVC.Views;

namespace CSHARPCRUDMVC.Presenters
{
    public class PetPresenter
    {
        //Fields
        private IPetView view;
        private IPetRepository repository;
        private BindingSource petBindingSource;
        private IEnumerable<PetModel> petList;

        //Constructor
        public PetPresenter(IPetView view, IPetRepository repository)
        {
            this.petBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            //Subscribe event handler methods to view events
            this.view.SearchEvent += SearchPet;
            this.view.AddNewEvent += AddNewPet;
            this.view.EditEvent += LoadSelectedPetToEdit;
            this.view.DeleteEvent += DeleteSelectedPet;
            this.view.SaveEvent += SavePet;
            this.view.CancelEvent += CancelAction;
            //Set pets Binding Source
            this.view.SetPetListBindingSource(petBindingSource);
            //Load pet list view
            LoadAllPetList();
            this.view.Show();
        }
        private void LoadAllPetList()
        {
            petList = repository.GetAll();
            petBindingSource.DataSource = petList; // Set data source.
        }
        private void SearchPet(Object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                petList = repository.GetByValue(this.view.SearchValue);
            else petList = repository.GetAll();
            petBindingSource.DataSource = petList;

        }

        private void AddNewPet(Object sender, EventArgs e)
        {
            view.IsEdit = false;
        }
        private void LoadSelectedPetToEdit(Object sender, EventArgs e)
        {
            var pet = (PetModel)petBindingSource.Current;
            view.PetId = pet.Id.ToString();
            view.PetName = pet.Name;
            view.PetType = pet.Type;
            view.PetColour = pet.Colour;
            view.IsEdit = true;
        }
        private void DeleteSelectedPet(Object sender, EventArgs e)
        {
            try
            {

                    var pet = (PetModel)petBindingSource.Current;
                    repository.Delete(pet.Id);
                    view.IsSuccessful = true;
                    view.Message = "Pet deleted Successfully";
                    LoadAllPetList();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                view.IsSuccessful = false;
                view.Message = "An error occured, could not delete pet";
            }

        }
        private void SavePet(Object sender, EventArgs e)
        {
            var model = new PetModel();
            model.Id = Convert.ToInt32(view.PetId);
            model.Name = view.PetName;
            model.Type = view.PetType;
            model.Colour = view.PetColour;
            try
            {
                new Common.ModelDataValidation().Validate(model);
                if (view.IsEdit)//Edit model
                {
                    repository.Edit(model);
                    view.Message = "Pet edited successfully";
                }
                else
                {
                    repository.Add(model);
                    view.Message = "Pet edited successfully";
                }
                view.IsSuccessful = true;
                LoadAllPetList();
                CleanviewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }


        }

        private void CleanviewFields()
        {
            view.PetId = "0";
            view.PetName = "";
            view.PetType = "";
            view.PetColour = "";

        }

        private void CancelAction(Object sender, EventArgs e)
        {
            CleanviewFields();

        }

    }
}
    

