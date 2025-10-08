function updateModal() {
    return {
        updateForm: { },

        open() {
            Alpine.store('globalState').updateModalOpen = true;
        },

        close() {
            Alpine.store('globalState').updateModalOpen = false;
            this.updateForm = { };
        },

        getActiveTab(){
          return Alpine.store('globalState').activeTab;  
        },

        async submit() {
            try {
                const store = Alpine.store('globalState');
                const token = store.currentUser.token;

                const url = this.getActiveTab() === 'hotels'
                    ? `http://localhost:8000/api/hotels/${id}`
                    : `http://localhost:8000/api/rooms/${id}`;

                const res = await fetch(url, {
                    method: 'PATCH',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`
                    },
                    body: JSON.stringify(this.updateForm)
                });

                if (!res.ok) throw new Error(await res.text());
                const data = await res.json();

                if (data.IsFailure) {
                    alert(data.Error);
                } else {
                    alert('Updated successfully!');
                }

                if (type === 'hotels') await store.fetchHotels();
                else await store.fetchRooms();

                this.close();
            } catch (err) {
                alert('Error: ' + err.message);
            }
        },
    }
}