package com.example.voiceacademyapp.adapter;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.example.voiceacademyapp.R;
import com.example.voiceacademyapp.activity.PodcastInfoActivity;
import com.example.voiceacademyapp.model.PodcastChannel;

import org.w3c.dom.Text;

import java.util.List;

public class PodcastAdapter extends RecyclerView.Adapter<PodcastAdapter.ViewHolder>{

    private List<PodcastChannel> podcastList;
    private Context context;
    private OnItemClickListener clickListener;

    public void setOnItemClickListener(OnItemClickListener listener) {
        this.clickListener = listener;
    }

    public PodcastAdapter(List<PodcastChannel> podcastList, Context context) {
        this.podcastList = podcastList;
        this.context = context;
    }

    @NonNull
    @Override
    public ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View podcastView = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_podcast, parent, false);
        return new ViewHolder(podcastView);
    }

    @Override
    public void onBindViewHolder(@NonNull ViewHolder holder, @SuppressLint("RecyclerView") int position) {
        PodcastChannel item = podcastList.get(position);

        holder.titlePodcast.setText(podcastList.get(position).getTitle());
        Glide.with(context).load(podcastList.get(position).getImagePodcast()).into(holder.imagePodcast);

        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if (clickListener != null) {
                    clickListener.onItemClick(position);
                }
            }
        });
        PodcastChannel podcast = podcastList.get(position);

        holder.btn_card.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                Intent intent = new Intent(context, PodcastInfoActivity.class);
                intent.putExtra("idPodcast", podcast.getIdPodcast());
                context.startActivity(intent);
            }
        });
    }

    @Override
    public int getItemCount() {
        return podcastList.size();
    }

    public class ViewHolder extends RecyclerView.ViewHolder {
        public View btn_card;
        private ImageView imagePodcast;
        private TextView titlePodcast;
        public ViewHolder(@NonNull View itemView) {
            super(itemView);
            imagePodcast = itemView.findViewById(R.id.iv_portada);
            titlePodcast = itemView.findViewById(R.id.tv_titulo);
            btn_card = itemView.findViewById(R.id.btn_card);
        }
    }
}
