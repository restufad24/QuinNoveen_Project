using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LadderCooldown : MonoBehaviour
{
    public float initialCooldownDuration = 30f; // Durasi cooldown awal dalam detik
    private float remainingCooldown; // Waktu yang tersisa dalam cooldown
    private bool isCooldownActive = false; // Status apakah cooldown sedang aktif atau tidak
    private float initialCooldownTime; // Waktu awal cooldown
    public TextMeshProUGUI cooldownTextMesh; // TextMeshProUGUI untuk menampilkan cooldown

    void Start()
    {
        // Set waktu awal cooldown dan waktu yang tersisa sama dengan durasi cooldown awal
        initialCooldownTime = initialCooldownDuration;
        remainingCooldown = initialCooldownDuration;
        UpdateCooldownText();
    }

    void Update()
    {
        // Jika tombol Space ditekan dan cooldown belum aktif
        if (Input.GetKeyDown(KeyCode.Space) && !isCooldownActive)
        {
            // Mulai cooldown
            StartCooldown();
        }

        // Hitung mundur waktu yang tersisa dalam cooldown jika cooldown sedang berlangsung
        if (isCooldownActive)
        {
            remainingCooldown -= Time.deltaTime;
            UpdateCooldownText();

            // Jika cooldown sudah selesai
            if (remainingCooldown <= 0f)
            {
                // Berhenti cooldown
                StopCooldown();
            }
        }
    }

    // Fungsi untuk memulai cooldown
    void StartCooldown()
    {
        isCooldownActive = true;
        remainingCooldown = initialCooldownDuration;
    }

    // Fungsi untuk menghentikan cooldown
    void StopCooldown()
    {
        isCooldownActive = false;
        remainingCooldown = initialCooldownDuration; // Reset remainingCooldown ke nilai awal
        UpdateCooldownText(); // Perbarui teks cooldown setelah direset
    }

    // Fungsi untuk memperbarui teks cooldown pada UI
    void UpdateCooldownText()
    {
        // Tampilkan sisa waktu cooldown dalam teks
        cooldownTextMesh.text = "" + Mathf.CeilToInt(remainingCooldown).ToString();
    }
}
